using StorageService.Api.Models;

using DomainModels = StorageService.Domain.Models;

namespace StorageService.Api.Mappers;

internal static class DatasetsDescriptionMapper
{
    internal static async Task<DatasetsDescription> ToApi(this IAsyncEnumerable<DomainModels.DatasetDescription> datasets, CancellationToken cancellationToken = default)
    {
        var apiDatasets = await datasets
            .Select(dataset => dataset.ToApi())
            .ToArrayAsync(cancellationToken);

        return new DatasetsDescription(apiDatasets);
    }
}