using StudyService.Api.Models.Studies;

using Study = StudyService.Domain.Models.Studies.Study;


namespace StudyService.Api.Mappers;

public static class StudiesMapper
{
    public static async Task<StudiesContainer> ToApi(this IAsyncEnumerable<Study> studies, CancellationToken cancellationToken )
    {
        var apiStudies = await studies
            .Select(study => study.ToApi())
            .ToArrayAsync(cancellationToken);
        
        return new StudiesContainer(
            Studies: apiStudies);
    }
}