namespace StudyService.Domain.Models;

public record StudyDataset(
    string Id,
    string Name,
    string[]? Columns);