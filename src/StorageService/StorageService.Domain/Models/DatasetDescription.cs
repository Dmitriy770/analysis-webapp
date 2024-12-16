namespace StorageService.Domain.Models;

public record DatasetDescription(
    Guid Id,
    string Name,
    long UserId)
{
    public string FileName => $"{UserId}_{Name}.csv";
}