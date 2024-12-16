namespace StudyService.Api.Models;

public record Study(
    Guid Id,
    string Type,
    string Status,
    int Components,
    StudyDataset Dataset);