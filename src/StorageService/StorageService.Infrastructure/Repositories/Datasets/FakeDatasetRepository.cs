using StorageService.Application.Abstractions.Repositories;
using StorageService.Domain.Models;

namespace StorageService.Infrastructure.Repositories.Datasets;

internal sealed class FakeDatasetRepository : IDatasetRepository
{
    public Task AddDescriptionAsync(DatasetDescription description, CancellationToken cancellationToken = default)
    {
        DatasetDescriptions.Add(description);
        
        return Task.CompletedTask;
    }

    public Task<DatasetDescription?> GetDescriptionAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var description = DatasetDescriptions.FirstOrDefault(description => description.Id == id);

        return Task.FromResult(description);
    }

    public IAsyncEnumerable<DatasetDescription> GetDescriptionsByUserIdAsync(long userId, CancellationToken cancellationToken = default)
    {
        return DatasetDescriptions
            .Where(description => description.UserId == userId)
            .ToAsyncEnumerable();
    }

    private static readonly List<DatasetDescription> DatasetDescriptions = [];
}