namespace Uis.Infrastructure.Gateways.GitHub.Models;

internal record User(
    long Id,
    string Login,
    string Name,
    Uri AvatarUrl);