namespace StorageService.Api.Models;

public record DatasetDescription(
    Guid Id,
    string Name,
    long UserId);