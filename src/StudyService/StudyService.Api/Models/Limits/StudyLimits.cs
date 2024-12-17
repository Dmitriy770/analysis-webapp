namespace StudyService.Api.Models.Limits;

public record StudyLimits(
    int Total,
    int Left,
    DateTime ReducesAt);