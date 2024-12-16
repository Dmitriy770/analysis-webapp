namespace StudyService.Api.Models;

public record NewStudyDataset(
    string Name,
    string[]? Columns);