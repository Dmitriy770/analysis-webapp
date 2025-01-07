using Microsoft.Extensions.Logging;
using StudyService.Application.Abstractions.Repositories;
using StudyService.Domain.Models.StudyResults;

namespace StudyServices.Infrastructure.Repositories.StudyResults;

internal sealed class FakeStudyResultRepository(
    ILogger<FakeStudyResultRepository> logger) : IStudyResultRepository
{
    public Task AddAsync(StudyResult studyResult, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Adding study result to database");
        
        StudyResults.Add(studyResult);
        
        return Task.CompletedTask;
    }

    public Task<StudyResult?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var studyResult = StudyResults.FirstOrDefault(studyResult => studyResult.StudyId == id);
        
        return Task.FromResult(studyResult);
    }

    private static readonly List<StudyResult> StudyResults = [];
}