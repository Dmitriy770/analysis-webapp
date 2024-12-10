namespace Uis.Domain.Models;

public record User(
    long Id,
    string Login,
    Uri AvatarUri,
    int Limit = 5);