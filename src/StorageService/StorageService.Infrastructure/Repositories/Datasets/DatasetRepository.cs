using Microsoft.EntityFrameworkCore;
using StorageService.Application.Abstractions.Repositories;
using StorageService.Domain.Models;

namespace StorageService.Infrastructure.Repositories.Datasets;

internal class DatasetRepository(
    DatasetRepositoryDbContext dbContext)
    : IDatasetRepository
{
    public async Task AddDescriptionAsync(DatasetDescription description, CancellationToken cancellationToken = default)
    {
        await dbContext.DatasetDescriptions.AddAsync(description, cancellationToken);
        
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<DatasetDescription?> GetDescriptionAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await dbContext.DatasetDescriptions
            .FirstOrDefaultAsync(description => description.Id == id, cancellationToken);
    }

    public IAsyncEnumerable<DatasetDescription> GetDescriptionsByUserIdAsync(long userId, CancellationToken cancellationToken = default)
    {
        return dbContext.DatasetDescriptions
            .Where(description => description.UserId == userId)
            .ToAsyncEnumerable();
    }
}