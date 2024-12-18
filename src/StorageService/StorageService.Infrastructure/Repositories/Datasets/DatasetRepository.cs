using Microsoft.EntityFrameworkCore;
using StorageService.Application.Abstractions.Repositories;
using StorageService.Domain.Models;
using StorageService.Infrastructure.Repositories.Datasets.Mappers;

namespace StorageService.Infrastructure.Repositories.Datasets;

internal class DatasetRepository(
    DatasetRepositoryDbContext dbContext)
    : IDatasetRepository
{
    public async Task AddDescriptionAsync(DatasetDescription description, CancellationToken cancellationToken = default)
    {
        await dbContext.DatasetDescriptions.AddAsync(description.ToInfrastructure(), cancellationToken);
        
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<DatasetDescription?> GetDescriptionAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var description =  await dbContext.DatasetDescriptions
            .FirstOrDefaultAsync(description => description.Id == id, cancellationToken);

        return description?.ToDomain();
    }

    public IAsyncEnumerable<DatasetDescription> GetDescriptionsByUserIdAsync(long userId, CancellationToken cancellationToken = default)
    {
        return dbContext.DatasetDescriptions
            .Where(description => description.UserId == userId)
            .ToAsyncEnumerable()
            .Select(description => description.ToDomain());
    }
}