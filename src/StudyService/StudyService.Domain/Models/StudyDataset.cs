namespace StudyService.Domain.Models;

public record StudyDataset(
    Guid Id,
    string Name,
    string[]? Columns);