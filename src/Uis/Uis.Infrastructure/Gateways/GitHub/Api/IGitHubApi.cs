using Refit;
using Uis.Infrastructure.Gateways.GitHub.Models;

namespace Uis.Infrastructure.Gateways.GitHub.Api;

internal interface IGitHubApi
{
    [Get("/user")]
    [Headers("X-GitHub-Api-Version: 2022-11-2", "Accept: application/json", "User-Agent: Uis.Api")]
    Task<User> GetUserAsync(
        [Header("Authorization")] string authorizationHeader);

    private const string ApiVersion = "2022-11-28";
}