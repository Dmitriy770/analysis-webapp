namespace StudyService.Domain.Models.StudyResults;

public record StudyResult(
    Guid StudyId,
    decimal[] Points);