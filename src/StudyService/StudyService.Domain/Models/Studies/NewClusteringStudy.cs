using StudyService.Domain.Models.StudyDatasets;

namespace StudyService.Domain.Models.Studies;

public record NewClusteringStudy(
    int Components,
    NewStudyDataset Dataset);