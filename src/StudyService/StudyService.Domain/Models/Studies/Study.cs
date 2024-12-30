using StudyService.Domain.Models.StudyDatasets;

namespace StudyService.Domain.Models.Studies;

public record Study(
    Guid Id,
    long UserId,
    StudyType Type,
    StudyStatus Status,
    int Components,
    DateTime CreationDate,
    StudyDataset Dataset);