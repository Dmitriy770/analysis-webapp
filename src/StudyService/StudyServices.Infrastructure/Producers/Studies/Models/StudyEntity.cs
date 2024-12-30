namespace StudyServices.Infrastructure.Producers.Studies.Models;

internal record StudyEntity(
    Guid Id,
    int Components,
    StudyDatasetEntity DatasetEntity);