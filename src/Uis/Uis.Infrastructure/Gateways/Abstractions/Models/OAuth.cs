namespace Uis.Infrastructure.Gateways.Abstractions.Models;

public record OAuth(
    string ClientId,
    string ClientSecret,
    string Code,
    Uri RedirectUri);