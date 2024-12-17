namespace StudyService.Domain.Models.StudyDatasets;

public record NewStudyDataset(
    string Name,
    string[]? Columns);