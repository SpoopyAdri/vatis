using System.Reflection;
using System.Threading.Tasks;
using GeoVR.Connection;
using GeoVR.Shared;
using Vatsim.Vatis.Common;
using Vatsim.Vatis.Config;

namespace Vatsim.Vatis.AudioForVatsim;

public class AudioManager : IAudioManager
{
    private readonly IAppConfig mAppConfig;
    private readonly ApiServerConnection mApiServerConnection;
    private string VoiceServerUrl = "https://voice1.vatsim.uk";

    public AudioManager(IAppConfig appConfig)
    {
        mAppConfig = appConfig;
        mApiServerConnection = new ApiServerConnection(VoiceServerUrl);
    }

    public async Task AddOrUpdateBot(byte[] audio, string callsign, uint frequency, double lat, double lon)
    {
        if (!mApiServerConnection.Authenticated)
        {
            await mApiServerConnection.Connect(mAppConfig.UserId,
                mAppConfig.Password, "vATIS " + Assembly.GetExecutingAssembly().GetName().Version);
        }

        await mApiServerConnection.RemoveBot(callsign).AwaitTimeout(5000);

        PutBotRequestDto dto = AtisBotHelper.AddBotRequest(audio, frequency, lat, lon, 100.0);

        await mApiServerConnection.AddOrUpdateBot(callsign, dto).AwaitTimeout(5000);
    }

    public async Task RemoveBot(string callsign)
    {
        await mApiServerConnection.RemoveBot(callsign).AwaitTimeout(5000);
    }
}