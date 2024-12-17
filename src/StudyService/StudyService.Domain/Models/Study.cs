namespace StudyService.Domain.Models;

public record Study(
    Guid Id,
    StudyType Type,
    string Status,
    int Components,
    DateTime CreationDate,
    StudyDataset Dataset);