using Refit;
using Uis.Infrastructure.Gateways.GitHub.Models;

namespace Uis.Infrastructure.Gateways.GitHub;

internal interface IGitHubApi
{
    [Get("/login/oauth/access_token")]
    Task<string> GetAccessTokenAsync(OAuth oAuth);
    
    [Get("/user")]
    Task<User> GetUserAsync([Header("Authorization")] string authorization);
}