using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading;
using System.Threading.Tasks;
using RestSharp;
using Vatsim.Vatis.Config;
using Vatsim.Vatis.Network;

namespace Vatsim.Vatis.TextToSpeech;

public class TextToSpeechRequest : ITextToSpeechRequest
{
    private readonly IAppConfig mAppConfig;
    private string mJwtToken;
    private DateTime mJwtValidTo;

    public TextToSpeechRequest(IAppConfig config)
    {
        mAppConfig = config;
    }

    public async Task<byte[]> RequestSynthesizedText(string text, CancellationToken token)
    {
        var client = new RestClient();

        if (string.IsNullOrEmpty(mJwtToken) || mJwtValidTo < DateTime.UtcNow)
        {
            try
            {
                var jwtRequest = new RestRequest("https://auth.vatsim.net/api/fsd-jwt", Method.POST)
                {
                    Timeout = 30000
                };
                jwtRequest.AddJsonBody(new PasswordTokenRequest(mAppConfig.VatsimId, mAppConfig.VatsimPasswordDecrypted));
                var jwtResponse = await client.ExecuteAsync<PasswordTokenResponse>(jwtRequest, token);
                if (jwtResponse != null)
                {
                    mJwtToken = jwtResponse.Data.token;
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

        var request = new RestRequest("https://tts.clowd.io/Request", Method.POST);
        request.AddJsonBody(new TextToSpeechRequestDto
        {
            Text = text,
            Voice = mAppConfig.CurrentComposite.AtisVoice.GetVoiceNameForRequest ?? "default",
            Jwt = mJwtToken
        });

        try
        {
            var response = await client.ExecuteAsync(request, token);
            if (response.StatusCode == (System.Net.HttpStatusCode)429)
            {
                throw new Exception(response.Content);
            }
            if (response.IsSuccessful)
            {
                return client.DownloadData(request);
            }
        }
        catch (TaskCanceledException) { }
        catch (OperationCanceledException) { }

        return null;
    }
}