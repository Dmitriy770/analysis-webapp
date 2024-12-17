namespace StudyService.Domain.Models.StudyDatasets;

public record StudyDataset(
    Guid Id,
    string Name,
    string[]? Columns);