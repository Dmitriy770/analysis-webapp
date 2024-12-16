namespace Uis.Domain.Models;

public record User(
    long Id,
    string Login,
    string Name,
    Uri AvatarUri,
    int Limit);