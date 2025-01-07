using StudyService.Application.Abstractions.Repositories;
using StudyService.Domain.Models.Studies;

namespace StudyServices.Infrastructure.Repositories.Studies;

internal sealed class FakeStudyRepository : IStudyRepository
{
    public Task AddAsync(Study study, CancellationToken cancellationToken = default)
    {
        Studies.Add(study);
        
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Study study, CancellationToken cancellationToken = default)
    {
        for (var i = 0; i < Studies.Count; i++)
        {
            if (Studies[i].Id == study.Id)
            {
                Studies[i] = study;
            }
        }
        
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