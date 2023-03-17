using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Vatsim.Vatis.Config;
using Vatsim.Vatis.Io;
using Vatsim.Vatis.Network;

namespace Vatsim.Vatis.TextToSpeech;

public class TextToSpeechRequest : ITextToSpeechRequest
{
    private const string FSD_JWT_ENDPOINT = "https://auth.vatsim.net/api/fsd-jwt";
    private const string TTS_SERVICE_ENDPOINT = "https://tts.clowd.io/Request";
    private readonly IAppConfig mAppConfig;
    private readonly IDownloader mDownloader;
    private string mJwtToken;
    private DateTime mJwtValidTo;

    public TextToSpeechRequest(IAppConfig config, IDownloader downloader)
    {
        mAppConfig = config;
        mDownloader = downloader;
    }

    public async Task<byte[]> RequestSynthesizedText(string text, CancellationToken token)
    {
        if (string.IsNullOrEmpty(mJwtToken) || mJwtValidTo < DateTime.UtcNow)
        {
            try
            {
                var requestBody = new PasswordTokenRequest(mAppConfig.UserId, mAppConfig.Password);
                var response = await mDownloader.PostJsonAsyncResponse<PasswordTokenResponse>(FSD_JWT_ENDPOINT, requestBody, token);
                if (response != null)
                {
                    mJwtToken = response.token;
                    var handler = new JwtSecurityTokenHandler();
                    var jwtToken = handler.ReadJwtToken(mJwtToken);
                    if (jwtToken != null)
                    {
                        mJwtValidTo = jwtToken.ValidTo.ToUniversalTime();
                    }
                }
            }
            catch (TaskCanceledException) { }
            catch (OperationCanceledException) { }
        }

        try
        {
            var ttsDto = new TextToSpeechRequestDto
            {
                Text = text,
                Voice = mAppConfig.CurrentComposite.AtisVoice.GetVoiceNameForRequest ?? "default",
                Jwt = mJwtToken
            };

            var ttsResponse = await mDownloader.PostJsonDownloadAsync(TTS_SERVICE_ENDPOINT, ttsDto, token);

            if (ttsResponse != null)
            {
                using var stream = new MemoryStream();
                await ttsResponse.CopyToAsync(stream, token);
                return stream.ToArray();
            }
        }
        catch (TaskCanceledException) { }
        catch (OperationCanceledException) { }

        return null;
    }
}