using StorageService.Domain.Models;

namespace StorageService.Application.Abstractions.Repositories;

public interface IDatasetRepository
{
    public Task AddDescriptionAsync(DatasetDescription description, CancellationToken cancellationToken = default);
    
    public Task<DatasetDescription?> GetDescriptionAsync(Guid id, CancellationToken cancellationToken = default);
    
    public IAsyncEnumerable<DatasetDescription> GetDescriptionsByUserIdAsync(long userId, CancellationToken cancellationToken = default);
}