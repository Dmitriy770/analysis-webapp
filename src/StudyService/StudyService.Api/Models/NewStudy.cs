namespace StudyService.Api.Models;

public record NewStudy(
    string Type,
    int Components,
    NewStudyDataset Dataset);