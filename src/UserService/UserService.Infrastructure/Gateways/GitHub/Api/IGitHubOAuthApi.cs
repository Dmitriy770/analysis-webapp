using Refit;
using Uis.Infrastructure.Gateways.GitHub.Models;

namespace Uis.Infrastructure.Gateways.GitHub.Api;

internal interface IGitHubOAuthApi
{
    [Get("/login/oauth/access_token")]
    [Headers("X-GitHub-Api-Version: 2022-11-28", "Accept: application/json", "User-Agent: UserService.Api")]
    Task<AccessTokenInfo> GetAccessTokenAsync(
        [Query]GetAccessTokenParams accessTokenParams);
}