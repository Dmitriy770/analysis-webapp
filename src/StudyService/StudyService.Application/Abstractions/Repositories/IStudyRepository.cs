using StudyService.Domain.Models;
using StudyService.Domain.Models.Studies;

namespace StudyService.Application.Abstractions.Repositories;

public interface IStudyRepository
{
    public Task AddAsync(Study study, CancellationToken cancellationToken = default);
    
    public Task UpdateAsync(Study study, CancellationToken cancellationToken = default);
    
    public IAsyncEnumerable<Study> GetByUserIdAsync(long userId, CancellationToken cancellationToken = default);
    
    public Task<Study?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}