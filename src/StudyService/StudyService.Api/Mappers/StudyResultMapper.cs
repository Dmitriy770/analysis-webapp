using StudyService.Api.Models.StudyResults;

namespace StudyService.Api.Mappers;

public static class StudyResultMapper
{
    public static StudyResult ToApi(this Domain.Models.StudyResults.StudyResult study)
    {
        return new StudyResult(
            Points: study.Points);
    }
}