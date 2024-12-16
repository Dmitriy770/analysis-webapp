namespace StudyService.Api.Models;

public record StudyDataset(
    string Id,
    string Name,
    string[]? Columns);