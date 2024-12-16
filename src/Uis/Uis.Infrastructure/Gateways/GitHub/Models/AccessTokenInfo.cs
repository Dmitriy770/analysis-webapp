using System.Text.Json.Serialization;

namespace Uis.Infrastructure.Gateways.GitHub.Models;

internal sealed class AccessTokenInfo
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; }
    
    [JsonPropertyName("token_type")]
    public string TokenType { get; set; }
    
    [JsonPropertyName("scope")]
    public string Scope { get; set; }
}