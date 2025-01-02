using System.Text.Json.Serialization;

namespace Uis.Infrastructure.Gateways.GitHub.Models;

internal sealed class User
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("login")]
    public string Login { get; set; }
    
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    [JsonPropertyName("avatar_url")]
    public Uri AvatarUrl { get; set; }
}