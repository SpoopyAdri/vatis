using System;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Vatsim.Vatis.Config;
using Vatsim.Vatis.Io;

namespace Vatsim.Vatis.Network;
public class AuthTokenManager : IAuthTokenManager
{
    private const string AUTH_TOKEN_URL = "https://auth.vatsim.net/api/fsd-jwt";
    private const double AUTH_TOKEN_SHELF_LIFE_MINUTES = 2.0;

    private readonly IDownloader mDownloader;
    private readonly IAppConfig mAppConfig;
    private string mAuthToken;
    private DateTime mAuthTokenGeneratedAt;

    public AuthTokenManager(IDownloader downloader, IAppConfig appConfig)
    {
        mDownloader = downloader;
        mAppConfig = appConfig;
    }

    public string AuthToken => mAuthToken;

    public async Task<string?> GetAuthToken()
    {
        if (mAuthToken != null && (DateTime.UtcNow - mAuthTokenGeneratedAt).TotalMinutes < AUTH_TOKEN_SHELF_LIFE_MINUTES)
        {
            return mAuthToken;
        }

        var requestJson = new JsonObject
        {
            ["cid"] = mAppConfig.UserId,
            ["password"] = mAppConfig.Password
        };

        var response = await mDownloader.PostJsonAsyncResponse<JsonObject>(AUTH_TOKEN_URL, requestJson);

        if (!(bool)(response["success"] ?? throw new ApplicationException("Authentication failed. \"success\" value is missing from response.")))
        {
            var errorMessage = response["error_msg"] ?? throw new ApplicationException("Authentication failed. No error message was provided.");
            throw new ApplicationException($"Authentication failed. {errorMessage}");
        }

        var token = response["token"] ?? throw new ApplicationException("Authentication failed. No authentication token was provided in the response.");

        mAuthToken = token.ToString();
        mAuthTokenGeneratedAt = DateTime.UtcNow;

        return mAuthToken;
    }
}