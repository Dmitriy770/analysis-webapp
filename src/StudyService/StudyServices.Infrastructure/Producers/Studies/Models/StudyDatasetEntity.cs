namespace StudyServices.Infrastructure.Producers.Studies.Models;

internal record StudyDatasetEntity(
    Guid Id,
    string Name,
    string[]? Columns);