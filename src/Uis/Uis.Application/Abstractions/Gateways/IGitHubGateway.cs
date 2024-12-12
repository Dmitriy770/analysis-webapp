using Uis.Application.Abstractions.Gateways.Models;

namespace Uis.Application.Abstractions.Gateways;

public interface IGitHubGateway
{
    Task<string> GetAccessTokenAsync(string code);
    
    Task<GitHubUser> GetUserAsync(string accessToken);
}