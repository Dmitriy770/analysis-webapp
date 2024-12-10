using Refit;
using Uis.Infrastructure.Gateways.Abstractions.Models;

namespace Uis.Infrastructure.Gateways.Abstractions;

public interface IGitHubGateway
{
    [Get("/login/oauth/access_token")]
    Task<string> GetAccessTokenAsync(OAuth oAuth);
    
    [Get("/user")]
    Task<User> GetUserAsync([Header("Authorization")] string authorization);
}