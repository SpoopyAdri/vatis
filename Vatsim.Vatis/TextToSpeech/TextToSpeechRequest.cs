using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Vatsim.Vatis.Config;
using Vatsim.Vatis.Io;
using Vatsim.Vatis.Network;

namespace Vatsim.Vatis.TextToSpeech;

public class TextToSpeechRequest : ITextToSpeechRequest
{
    private const string TTS_SERVICE_ENDPOINT = "https://tts.clowd.io/Request";
    private readonly IAppConfig mAppConfig;
    private readonly IDownloader mDownloader;
    private readonly IAuthTokenManager mAuthTokenManager;

    public TextToSpeechRequest(IAppConfig config, IDownloader downloader, IAuthTokenManager authTokenManager)
    {
        mAppConfig = config;
        mDownloader = downloader;
        mAuthTokenManager = authTokenManager;
    }

    public async Task<byte[]> RequestSynthesizedText(string text, CancellationToken token)
    {
        var authToken = await mAuthTokenManager.GetAuthToken();

        try
        {
            var ttsDto = new TextToSpeechRequestDto
            {
                Text = text,
                Voice = mAppConfig.Voices.FirstOrDefault(n => n.Name == mAppConfig.CurrentComposite.AtisVoice.Voice).Id ?? "default",
                Jwt = authToken
            };

            var ttsResponse = await mDownloader.PostJsonDownloadAsync(TTS_SERVICE_ENDPOINT, ttsDto, token);

            if (ttsResponse != null)
            {
                using var stream = new MemoryStream();
                await ttsResponse.CopyToAsync(stream, token);
                return stream.ToArray();
            }
        }
        catch (OperationCanceledException) { }

        return null;
    }
}