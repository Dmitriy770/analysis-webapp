using Refit;

namespace Uis.Infrastructure.Gateways.GitHub.Models;

internal sealed class AccessTokenInfo
{
    [AliasAs("access_token")]
    public string AccessToken { get; set; }
    
    [AliasAs("scope")]
    public string Scope { get; set; }
    
    [AliasAs("token_type")]
    public string TokenType { get; set; }
    
}