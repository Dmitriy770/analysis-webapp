namespace StudyService.Domain.Models;

public record Study(
    Guid Id,
    StudyType Type,
    StudyStatus Status,
    int Components,
    DateTime CreationDate,
    StudyDataset Dataset);