namespace Uis.Domain.Models;

public record Session(
    Guid SessionId,
    long UserId,
    DateTime CreatedDateTime);