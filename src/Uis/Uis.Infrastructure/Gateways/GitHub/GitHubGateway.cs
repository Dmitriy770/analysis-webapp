using System.Net.Http.Headers;
using Uis.Application.Abstractions.Gateways;
using Uis.Application.Abstractions.Gateways.Models;
using Uis.Infrastructure.Gateways.GitHub.Api;
using Uis.Infrastructure.Settings;

namespace Uis.Infrastructure.Gateways.GitHub;

internal class GitHubGateway(
    IGitHubApi gitHubApi,
    IGitHubOAuthApi gitHubOAuthApi,
    GitHubGatewaySettings settings)
    : IGitHubGateway
{
    public async Task<string> GetAccessTokenAsync(string code)
    {
        var accessTokenInfo = await gitHubOAuthApi.GetAccessTokenAsync(
            settings.ClientId,
            settings.ClientSecret,
            code,
            settings.RedirectUri);
        
        return accessTokenInfo.AccessToken;
    }

    public async Task<GitHubUser> GetUserAsync(string accessToken)
    {
        var authenticationHeader = new AuthenticationHeaderValue("Bearer ", accessToken);
        var user = await gitHubApi.GetUserAsync(authenticationHeader.ToString());

        return new GitHubUser(
            Id: user.Id,
            Login: user.Login,
            Name: user.Name,
            AvatarUrl: user.AvatarUrl);
    }
}