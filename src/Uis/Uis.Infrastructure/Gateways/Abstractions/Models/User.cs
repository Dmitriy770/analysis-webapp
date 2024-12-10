namespace Uis.Infrastructure.Gateways.Abstractions.Models;

public record User(
    long Id,
    string Login,
    string Name,
    Uri AvatarUrl);