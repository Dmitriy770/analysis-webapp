using StudyService.Domain.Models;

namespace StudyService.Application.Abstractions.Repositories;

public interface IStudyRepository
{
    public Task AddAsync(Study study, CancellationToken cancellationToken = default);
    
    public IAsyncEnumerable<Study> GetByUserIdAsync(long userId, CancellationToken cancellationToken = default);
}