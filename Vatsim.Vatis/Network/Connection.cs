using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Timers;
using RestSharp;
using Vatsim.Network;
using Vatsim.Network.PDU;
using Vatsim.Vatis.Common;
using Vatsim.Vatis.Config;
using Vatsim.Vatis.Core;
using Vatsim.Vatis.Events;
using Vatsim.Vatis.UI.Dialogs;

namespace Vatsim.Vatis.Network;

public class Connection
{
    public string AirportIcao { get; set; }
    public int Frequency { get; set; }
    public string Callsign => Composite.AtisType switch
    {
        AtisType.Departure => $"{AirportIcao}_D_ATIS",
        AtisType.Arrival => $"{AirportIcao}_A_ATIS",
        _ => $"{AirportIcao}_ATIS",
    };
    public bool IsConnected => mSession.Connected;
    public AtisComposite Composite { get; set; }

    public event EventHandler NetworkConnectedChanged;
    public event EventHandler NetworkDisconnectedChanged;
    public event EventHandler<MetarResponseReceived> MetarResponseReceived;
    public event EventHandler<NetworkErrorReceived> NetworkErrorReceived;
    public event EventHandler<KillRequestReceived> KillRequestReceived;

    private readonly FSDSession mSession;
    private readonly IAppConfig mAppConfig;
    private readonly INavaidDatabase mAirportDatabase;
    private readonly ClientProperties mClientProperties;
    private readonly string mVolumeSerial;
    private System.Timers.Timer mPositionUpdateTimer;
    private System.Timers.Timer mMetarUpdateTimer;
    private string mPublicIp;
    private string mPreviousMetar;
    private Airport mAirport;
    private string mPasswordToken;
    private List<string> mSubscribers = new List<string>();
    private List<string> mEuroscopeSubscribers = new List<string>();
    private List<string> mCapsReceived = new List<string>();

    public Connection(IAppConfig config, INavaidDatabase airportDatabase)
    {
        mAppConfig = config;
        mAirportDatabase = airportDatabase;

        mClientProperties = new ClientProperties("vATIS", Assembly.GetExecutingAssembly().GetName().Version, Environment.ProcessPath.GetCheckSum());

        mPositionUpdateTimer = new System.Timers.Timer();
        mPositionUpdateTimer.Interval = 15000;
        mPositionUpdateTimer.Elapsed += OnPositionUpdateTimerElapsed;

        mMetarUpdateTimer = new System.Timers.Timer();
        mMetarUpdateTimer.Interval = 300000;
        mMetarUpdateTimer.Elapsed += OnMetarUpdateTimerElapsed;

        try
        {
            mVolumeSerial = SystemIdentifier.GetSystemDriveVolumeId();
        }
        catch
        {
            mVolumeSerial = "Unknown";
        }

        mSession = new FSDSession(mClientProperties);
        mSession.IgnoreUnknownPackets = true;
        mSession.NetworkConnected += OnNetworkConnected;
        mSession.NetworkDisconnected += OnNetworkDisconnected;
        mSession.NetworkError += OnNetworkError;
        mSession.ProtocolErrorReceived += OnProtocolErrorReceived;
        mSession.ServerIdentificationReceived += OnServerIdentificationReceived;
        mSession.ClientQueryReceived += OnClientQueryReceived;
        mSession.ClientQueryResponseReceived += OnClientQueryResponseReceived;
        mSession.AcarsResponseReceived += OnAcarsResponseReceived;
        mSession.KillRequestReceived += OnKillRequestReceived;
        mSession.TextMessageReceived += OnTextMessageReceived;
        mSession.ATCPositionReceived += OnATCPositionReceived;
    }

    private void OnATCPositionReceived(object sender, DataReceivedEventArgs<PDUATCPosition> e)
    {
        if (!mCapsReceived.Contains(e.PDU.From))
        {
            mSession.SendPDU(new PDUClientQuery(Callsign, e.PDU.From, ClientQueryType.Capabilities, new List<string> { }));
        }
    }

    private void OnTextMessageReceived(object sender, DataReceivedEventArgs<PDUTextMessage> e)
    {
        var from = e.PDU.From.ToUpper();
        var msg = e.PDU.Message.ToUpper();

        switch (msg)
        {
            case "SUBSCRIBE":
                if (!mSubscribers.Contains(from))
                {
                    mSubscribers.Add(from);
                    mSession.SendPDU(new PDUTextMessage(Callsign, from, $"You are now subscribed to receive {Callsign} update notifications. To stop receiving these notifications, reply or send a private message to {Callsign} with the message UNSUBSCRIBE."));
                }
                else
                {
                    mSession.SendPDU(new PDUTextMessage(Callsign, from, $"You are already subscribed to {Callsign} update notifications. To stop receiving these notifications, reply or send a private message to {Callsign} with the message UNSUBSCRIBE."));
                }
                break;
            case "UNSUBSCRIBE":
                if (mSubscribers.Contains(from))
                {
                    mSubscribers.Remove(from);
                    mSession.SendPDU(new PDUTextMessage(Callsign, from, $"You have been unsubscribed from {Callsign} update notifications. You may subscribe agian by sending a private message to {Callsign} with the message SUBSCRIBE."));
                }
                break;
        }
    }

    private void OnKillRequestReceived(object sender, DataReceivedEventArgs<PDUKillRequest> e)
    {
        KillRequestReceived?.Invoke(this, new KillRequestReceived(e.PDU.Reason));
        Disconnect();
    }

    private void OnProtocolErrorReceived(object sender, DataReceivedEventArgs<PDUProtocolError> e)
    {
        if (e.PDU.Fatal)
        {
            NetworkErrorReceived?.Invoke(this, new NetworkErrorReceived(e.PDU.Message));
        }
    }

    private void OnNetworkError(object sender, NetworkErrorEventArgs e)
    {
        NetworkErrorReceived?.Invoke(this, new NetworkErrorReceived(e.Error));
    }

    private void OnAcarsResponseReceived(object sender, DataReceivedEventArgs<PDUMetarResponse> e)
    {
        if (mPreviousMetar != e.PDU.Metar)
        {
            bool isNewMetar = false;
            if (!string.IsNullOrEmpty(mPreviousMetar))
            {
                isNewMetar = true;
            }
            MetarResponseReceived?.Invoke(this, new MetarResponseReceived(e.PDU.Metar, isNewMetar));
        }
        mPreviousMetar = e.PDU.Metar;
    }

    private void OnClientQueryResponseReceived(object sender, DataReceivedEventArgs<PDUClientQueryResponse> e)
    {
        switch (e.PDU.QueryType)
        {
            case ClientQueryType.PublicIP:
                mPublicIp = ((e.PDU.Payload.Count > 0) ? e.PDU.Payload[0] : "");
                break;
            case ClientQueryType.Capabilities:
                if (!mCapsReceived.Contains(e.PDU.From))
                {
                    mCapsReceived.Add(e.PDU.From);
                }
                if (e.PDU.Payload.Contains("ONGOINGCOORD=1"))
                {
                    // Euroscope Subscribers
                    if (!mEuroscopeSubscribers.Contains(e.PDU.From))
                    {
                        mEuroscopeSubscribers.Add(e.PDU.From);
                    }
                }
                break;
        }
    }

    private void OnClientQueryReceived(object sender, DataReceivedEventArgs<PDUClientQuery> e)
    {
        switch (e.PDU.QueryType)
        {
            case ClientQueryType.Capabilities:
                mSession.SendPDU(new PDUClientQueryResponse(Callsign, e.PDU.From, ClientQueryType.Capabilities, new List<string>()
                {
                    "VERSION=1",
                    "ATCINFO=1"
                }));
                break;
            case ClientQueryType.RealName:
                mSession.SendPDU(new PDUClientQueryResponse(Callsign, e.PDU.From, ClientQueryType.RealName, new List<string>()
                {
                    mAppConfig.Name,
                    $"vATIS connection for {AirportIcao}",
                    ((int)mAppConfig.NetworkRating).ToString()
                }));
                break;
            case ClientQueryType.ATIS:
                int num = 0;
                if (!string.IsNullOrEmpty(Composite.AcarsText))
                {
                    // break up the text into 64 characters per line
                    var regex = new System.Text.RegularExpressions.Regex(@"(.{1,64})(?:\s|$)");
                    var collection = regex.Matches(Composite.AcarsText)
                        .Cast<System.Text.RegularExpressions.Match>()
                        .Select(x => x.Groups[1].Value)
                        .ToList();
                    foreach (var line in collection)
                    {
                        num++;
                        mSession.SendPDU(new PDUClientQueryResponse(Callsign, e.PDU.From, ClientQueryType.ATIS, new List<string> { "T", line.ToUpper() }));
                    }
                }
                num++;
                mSession.SendPDU(new PDUClientQueryResponse(Callsign, e.PDU.From, ClientQueryType.ATIS, new List<string> { "E", num.ToString() }));
                break;
            case ClientQueryType.INF:
                var msg = $"CID={mAppConfig.UserId} {mClientProperties.Name} {mClientProperties.Version} IP={mPublicIp} SYS_UID={mVolumeSerial} FSVER=N/A LT=0.00 LO=0.00 AL=50 {mAppConfig.Name}";
                mSession.SendPDU(new PDUClientQueryResponse(Callsign, e.PDU.From, ClientQueryType.INF, new List<string>()
                {
                    msg
                }));
                break;
        }
    }

    private void OnPositionUpdateTimerElapsed(object sender, ElapsedEventArgs e)
    {
        SendAtcPositionPacket();
    }

    private void OnMetarUpdateTimerElapsed(object sender, ElapsedEventArgs e)
    {
        RequestMetar();
    }

    private void SendAtcPositionPacket()
    {
        mSession.SendPDU(new PDUATCPosition(
            Callsign,
            Frequency,
            NetworkFacility.TWR,
            50,
            mAppConfig.NetworkRating,
            mAirport.Latitude,
            mAirport.Longitude));
    }

    public async void Connect()
    {
        if (SetPasswordToken(mAppConfig.UserId, mAppConfig.Password))
        {
            mAirport = mAirportDatabase.GetAirport(AirportIcao);
            if (mAirport == null)
                throw new Exception("Airport not found: " + AirportIcao);

            var server = mAppConfig.CachedServers.FirstOrDefault(t => t.Name == mAppConfig.ServerName);

            if (server != null)
            {
                var serverAddress = server.Address;
                if (mAppConfig.ServerName == "AUTOMATIC")
                {
                    try
                    {
                        var bestFsdServer = await new HttpClient().GetStringAsync("http://fsd-http.connect.vatsim.net");
                        if (!string.IsNullOrEmpty(bestFsdServer))
                        {
                            if (Regex.IsMatch(bestFsdServer, @"^\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}$"))
                            {
                                serverAddress = bestFsdServer;
                            }
                        }
                    }
                    catch { }
                }
                mSession.Connect(serverAddress, 6809);
                mPreviousMetar = "";
            }
            else
            {
                throw new Exception("Please choose VATSIM server in Settings.");
            }
        }
    }

    private bool SetPasswordToken(string cid, string password)
    {
        var client = new RestClient();
        var request = new RestRequest("https://auth.vatsim.net/api/fsd-jwt");
        request.Timeout = 5000;
        request.AddJsonBody(new PasswordTokenRequest(cid, password));
        var response = client.Post<PasswordTokenResponse>(request);
        if (response.IsSuccessful)
        {
            if (response.Data != null)
            {
                mPasswordToken = response.Data.token;
                return true;
            }
            NetworkErrorReceived?.Invoke(this, new NetworkErrorReceived(response.Data.error_msg));
            return false;
        }
        else
        {
            NetworkErrorReceived?.Invoke(this, new NetworkErrorReceived(response.ErrorMessage ?? "Could not get password token."));
            return false;
        }
    }

    public void Disconnect()
    {
        mSession.SendPDU(new PDUDeleteATC(Callsign, mAppConfig.UserId));
        mSession.Disconnect();
        mMetarUpdateTimer.Stop();
        mPositionUpdateTimer.Stop();
        mPreviousMetar = "";
        mPasswordToken = null;
        mCapsReceived.Clear();
        mEuroscopeSubscribers.Clear();
    }

    private void OnServerIdentificationReceived(object sender,
        DataReceivedEventArgs<PDUServerIdentification> e)
    {
        SendClientIdentification();
        SendAddAtc();
        SendAtcPositionPacket();
        RequestMetar();

        mPositionUpdateTimer.Start();
        mMetarUpdateTimer.Start();
    }

    private void SendClientIdentification()
    {
        mSession.SendPDU(new PDUClientIdentification(
            Callsign,
            mSession.GetClientKey(),
            mClientProperties.Name,
            mClientProperties.Version.Major,
            mClientProperties.Version.Minor,
            mAppConfig.UserId,
            mVolumeSerial,
            null));
    }

    private void SendAddAtc()
    {
        mSession.SendPDU(new PDUAddATC(
            AddATCPositionType.ATIS,
            Callsign,
            mAppConfig.Name,
            mAppConfig.UserId,
            string.IsNullOrEmpty(mPasswordToken) ? mAppConfig.Password : mPasswordToken,
            mAppConfig.NetworkRating,
            ProtocolRevision.VatsimAuth));

        mSession.SendPDU(new PDUClientQuery(
            Callsign,
            PDUBase.SERVER_CALLSIGN,
            ClientQueryType.PublicIP,
            null));
    }

    private void RequestMetar()
    {
        mSession.SendPDU(new PDUMetarRequest(Callsign, AirportIcao));
    }

    private void OnNetworkConnected(object sender, NetworkEventArgs e)
    {
        NetworkConnectedChanged?.Invoke(this, EventArgs.Empty);
    }

    private void OnNetworkDisconnected(object sender, NetworkEventArgs e)
    {
        NetworkDisconnectedChanged?.Invoke(this, EventArgs.Empty);
        mPreviousMetar = "";
        mPasswordToken = null;
    }

    public void SendSubscriberNotification()
    {
        foreach (var subscriber in mSubscribers)
        {
            mSession.SendPDU(new PDUTextMessage(Callsign, subscriber, $"***{AirportIcao} ATIS UPDATE: {Composite.CurrentAtisLetter} {Composite.DecodedMetar.SurfaceWind} - {Composite.DecodedMetar.Pressure}"));
        }

        foreach (var subscriber in mEuroscopeSubscribers)
        {
            mSession.SendPDU(new PDUTextMessage(Callsign, subscriber, $"ATIS info:{AirportIcao}:{Composite.CurrentAtisLetter}:"));
        }

        mSession.SendPDU(new PDUClientQuery(Callsign, PDUBase.CLIENT_QUERY_BROADCAST_RECIPIENT, ClientQueryType.NewATIS, new List<string> { $"ATIS {Composite.CurrentAtisLetter}:  {Composite.DecodedMetar.SurfaceWind} - {Composite.DecodedMetar.Pressure}" }));
    }
}

internal class PasswordTokenResponse
{
    public bool success { get; set; }
    public string error_msg { get; set; }
    public string token { get; set; }
}

internal class PasswordTokenRequest
{
    public string cid { get; set; }
    public string password { get; set; }
    public PasswordTokenRequest(string id, string pass)
    {
        cid = id;
        password = pass;
    }
}