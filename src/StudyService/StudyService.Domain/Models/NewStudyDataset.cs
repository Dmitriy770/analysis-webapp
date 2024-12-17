namespace StudyService.Domain.Models;

public record NewStudyDataset(
    string Name,
    string[]? Columns);