namespace Uis.Infrastructure.Repositories.Abstractions.Models;

public record User(
    long Id,
    string Login,
    Uri AvatarUri,
    int Limit);