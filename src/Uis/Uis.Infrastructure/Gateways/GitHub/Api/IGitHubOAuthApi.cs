using Refit;
using Uis.Infrastructure.Gateways.GitHub.Models;

namespace Uis.Infrastructure.Gateways.GitHub.Api;

internal interface IGitHubOAuthApi
{
    [Get("/login/oauth/access_token")]
    Task<AccessTokenInfo> GetAccessTokenAsync(
        [Query("client_id")] string clientId,
        [Query("client_secret")] string clientSecret,
        [Query("code")] string code,
        [Query("redirect_uri")] string redirectUri,
        [Header("X-GitHub-Api-Version")] string apiVersion = ApiVersion);
    
    private const string ApiVersion = "2022-11-28";
}