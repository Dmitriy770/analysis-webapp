using StudyService.Api.Models.Limits;
using DomainModels = StudyService.Domain.Models.Limits;

namespace StudyService.Api.Mappers;

public static class StudyLimitsMapper
{
    public static StudyLimits ToApi(this DomainModels.StudyLimits limits)
    {
        return new StudyLimits(
            Total: limits.Total,
            Left: limits.Left,
            ReducesAt: limits.ReducesAt);
    }
}