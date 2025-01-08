namespace Uis.Application.Abstractions.Gateways.Models;

public record GitHubUser(
    long Id,
    string Login,
    string Name,
    Uri AvatarUrl);