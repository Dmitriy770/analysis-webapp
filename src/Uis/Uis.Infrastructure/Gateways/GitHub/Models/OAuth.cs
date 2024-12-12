namespace Uis.Infrastructure.Gateways.GitHub.Models;

internal record OAuth(
    string ClientId,
    string ClientSecret,
    string Code,
    Uri RedirectUri);