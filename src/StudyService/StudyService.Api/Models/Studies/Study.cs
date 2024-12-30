using StudyService.Api.Models.StudyDatasets;

namespace StudyService.Api.Models.Studies;

public record Study(
    Guid Id,
    string Type,
    string Status,
    int Components,
    DateTime CreationDate,
    StudyDataset Dataset);