namespace Uis.Infrastructure.Repositories.Abstractions.Models;

public record Session(
    Guid SessionId,
    long UserId,
    DateTime CreatedDate);