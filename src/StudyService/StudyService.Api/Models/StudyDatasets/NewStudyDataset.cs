namespace StudyService.Api.Models.StudyDatasets;

public record NewStudyDataset(
    string Name,
    string[]? Columns);