using Refit;
using Uis.Infrastructure.Gateways.GitHub.Models;

namespace Uis.Infrastructure.Gateways.GitHub.Api;

internal interface IGitHubApi
{
    [Get("/user")]
    Task<User> GetUserAsync(
        [Header("Authorization")] string authorizationHeader,
        [Header("X-GitHub-Api-Version")] string apiVersion = ApiVersion);

    private const string ApiVersion = "2022-11-28";
}