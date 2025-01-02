using Refit;

namespace Uis.Infrastructure.Gateways.GitHub.Models;

internal sealed class GetAccessTokenParams
{
    [AliasAs("client_id")]
    public string ClientId { get; set; }
    
    [AliasAs("client_secret")]
    public string ClientSecret { get; set; }
    
    [AliasAs("code")]
    public string Code { get; set; }
    
    [AliasAs("redirect_uri")]
    public string RedirectUri { get; set; }
}