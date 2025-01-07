using Microsoft.Extensions.Logging;
using StudyService.Application.Abstractions.Repositories;
using StudyService.Domain.Models.Studies;

namespace StudyServices.Infrastructure.Repositories.Studies;

internal sealed class FakeStudyRepository(
    ILogger<FakeStudyRepository> logger) : IStudyRepository
{
    public Task AddAsync(Study study, CancellationToken cancellationToken = default)
    {
        Studies.Add(study);
        
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Study study, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Updating study {studyId}", study.Id);
        
        for (var i = 0; i < Studies.Count; i++)
        {
            if (Studies[i].Id == study.Id)
            {
                Studies[i] = study;
            }
        }
        
        logger.LogInformation("Updated {study}", Studies.FirstOrDefault(currentStudy => currentStudy.Id == study.Id));
        
        return Task.CompletedTask;
    }

    public IAsyncEnumerable<Study> GetByUserIdAsync(long userId, CancellationToken cancellationToken = default)
    {
        return Studies
            .Where(study => study.UserId == userId)
            .ToAsyncEnumerable();
    }

    public Task<Study?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var study = Studies.FirstOrDefault(study => study.Id == id);
        
        return Task.FromResult(study);
    }
    
    private static readonly List<Study> Studies = [];
}