namespace StudyService.Domain.Models.StudyResults;

public record StudyResult(
    Guid StudyId,
    string[][] Points);