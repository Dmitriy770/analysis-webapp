namespace StudyService.Domain.Models.Limits;

public record StudyLimits(
    int Total,
    int Left,
    DateTime ReducesAt);