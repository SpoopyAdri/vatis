using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Timers;
using Vatsim.Network;
using Vatsim.Network.PDU;
using Vatsim.Vatis.Config;
using Vatsim.Vatis.Events;
using Vatsim.Vatis.Io;
using Vatsim.Vatis.NavData;
using Vatsim.Vatis.Profiles;
using Vatsim.Vatis.UI.Dialogs;
using Vatsim.Vatis.Utils;

namespace Vatsim.Vatis.Network;

public class Connection
{
    public string AirportIcao { get; set; }
    public int Frequency => (int)((Composite.Frequency / 1000) - 100000);
    public string Callsign => Composite.AtisType switch
    {
        AtisType.Departure => $"{AirportIcao}_D_ATIS",
        AtisType.Arrival => $"{AirportIcao}_A_ATIS",
        _ => $"{AirportIcao}_ATIS",
    };
    public bool IsConnected => mSession.Connected;
    public Composite Composite { get; set; }

    public event EventHandler NetworkConnectedChanged;
    public event EventHandler NetworkDisconnectedChanged;
    public event EventHandler<MetarResponseReceived> MetarResponseReceived;
    public event EventHandler MetarNotFoundReceived;
    public event EventHandler<NetworkErrorReceived> NetworkErrorReceived;
    public event EventHandler<KillRequestReceived> KillRequestReceived;
    public event EventHandler<ClientEventArgs<string>> ChangeServerReceived;

    private const string VATDNS_ENDPOINT = "http://fsd.vatsim.net";
    private readonly FSDSession mSession;
    private readonly IAppConfig mAppConfig;
    private readonly INavDataRepository mAirportDatabase;
    private readonly IDownloader mDownloader;
    private readonly IAuthTokenManager mAuthTokenManager;
    private readonly ClientProperties mClientProperties;
    private readonly string mVolumeSerial;
    private Timer mPositionUpdateTimer;
    private Timer mMetarUpdateTimer;
    private string mPublicIp;
    private string mPreviousMetar;
    private Airport mAirport;
    private List<string> mSubscribers = new List<string>();
    private List<string> mEuroscopeSubscribers = new List<string>();
    private List<string> mCapsReceived = new List<string>();

    public Connection(IAppConfig config, INavDataRepository airportDatabase, IDownloader downloader, IAuthTokenManager authTokenManager)
    {
        mAppConfig = config;
        mAirportDatabase = airportDatabase;
        mDownloader = downloader;
        mAuthTokenManager = authTokenManager;

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
        mSession.ChangeServerReceived += OnChangeServerReceived;
    }

    private void OnChangeServerReceived(object sender, DataReceivedEventArgs<PDUChangeServer> e)
    {
        ChangeServerReceived?.Invoke(this, new ClientEventArgs<string>(e.PDU.NewServer));
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
        if (e.PDU.ErrorType == NetworkError.NoWeatherProfile)
        {
            MetarNotFoundReceived?.Invoke(this, EventArgs.Empty);
        }
        else if (e.PDU.Fatal)
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
                if (!string.IsNullOrEmpty(Composite.TextAtis))
                {
                    // break up the text into 64 characters per line
                    var regex = new Regex(@"(.{1,64})(?:\s|$)");
                    var collection = regex.Matches(Composite.TextAtis)
                        .Cast<Match>()
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
        try
        {
            await mAuthTokenManager.GetAuthToken();
        }
        catch (Exception ex)
        {
            NetworkErrorReceived?.Invoke(this, new NetworkErrorReceived(ex.Message));
            return;
        }

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
                    var bestFsdServer = await mDownloader.DownloadStringAsync(VATDNS_ENDPOINT);
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

    public void Disconnect()
    {
        mSession.SendPDU(new PDUDeleteATC(Callsign, mAppConfig.UserId));
        mSession.Disconnect();
        mMetarUpdateTimer.Stop();
        mPositionUpdateTimer.Stop();
        mPreviousMetar = "";
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
            string.IsNullOrEmpty(mAuthTokenManager.AuthToken) ? mAppConfig.Password : mAuthTokenManager.AuthToken,
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
    }

    public void SendSubscriberNotification()
    {
        if (Composite.DecodedMetar == null)
            return;

        foreach (var subscriber in mSubscribers)
        {
            mSession.SendPDU(new PDUTextMessage(Callsign, subscriber, $"***{AirportIcao} ATIS UPDATE: {Composite.AtisLetter} {Composite.DecodedMetar.SurfaceWind?.RawValue} - {Composite.DecodedMetar.AltimeterSetting?.RawValue}"));
        }

        foreach (var subscriber in mEuroscopeSubscribers)
        {
            mSession.SendPDU(new PDUTextMessage(Callsign, subscriber, $"ATIS info:{AirportIcao}:{Composite.AtisLetter}:"));
        }

        mSession.SendPDU(new PDUClientQuery(Callsign, PDUBase.CLIENT_QUERY_BROADCAST_RECIPIENT, ClientQueryType.NewATIS, new List<string> { $"ATIS {Composite.AtisLetter}:  {Composite.DecodedMetar.SurfaceWind?.RawValue} {Composite.DecodedMetar.AltimeterSetting?.RawValue}" }));
    }
}
