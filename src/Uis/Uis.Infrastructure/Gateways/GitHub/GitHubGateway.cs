using Uis.Application.Abstractions.Gateways;
using Uis.Application.Abstractions.Gateways.Models;
using Uis.Infrastructure.Gateways.GitHub.Models;
using Uis.Infrastructure.Settings;

namespace Uis.Infrastructure.Gateways.GitHub;

internal class GitHubGateway(
    IGitHubApi gitHubApi,
    GitHubGatewaySettings settings)
    : IGitHubGateway
{
    public async Task<string> GetAccessTokenAsync(string code)
    {
        var oauth = new OAuth(
            ClientId: settings.ClientId,
            ClientSecret: settings.ClientSecret,
            RedirectUri: settings.RedirectUri,
            Code: code);
        
        return await gitHubApi.GetAccessTokenAsync(oauth);
    }

    public async Task<GitHubUser> GetUserAsync(string accessToken)
    {
        var user = await gitHubApi.GetUserAsync(accessToken);

        return new GitHubUser(
            Id: user.Id,
            Login: user.Login,
            Name: user.Name,
            AvatarUrl: user.AvatarUrl);
    }
}