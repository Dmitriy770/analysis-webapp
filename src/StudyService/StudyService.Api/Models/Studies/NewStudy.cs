using StudyService.Api.Models.StudyDatasets;

namespace StudyService.Api.Models.Studies;

public record NewStudy(
    string Type,
    int Components,
    NewStudyDataset Dataset);