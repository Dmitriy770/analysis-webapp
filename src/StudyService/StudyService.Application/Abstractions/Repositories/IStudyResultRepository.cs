using StudyService.Domain.Models;
using StudyService.Domain.Models.StudyResults;

namespace StudyService.Application.Abstractions.Repositories;

public interface IStudyResultRepository
{
    public Task AddAsync(StudyResult studyResult, CancellationToken cancellationToken = default);
    
    public Task<StudyResult?> GetAsync(Guid id, CancellationToken cancellationToken = default);
}