using StorageService.Domain.Models;
using StorageService.Infrastructure.Repositories.Datasets.Models;

namespace StorageService.Infrastructure.Repositories.Datasets.Mappers;

internal static class DatasetDescriptionMapper
{
    internal static DatasetDescriptionEntity ToInfrastructure(this DatasetDescription datasetDescription)
    {
        return new DatasetDescriptionEntity
        {
            Id = datasetDescription.Id,
            Name = datasetDescription.Name,
            UserId = datasetDescription.UserId
        };
    }
    
    internal static DatasetDescription ToDomain(this DatasetDescriptionEntity datasetDescription)
    {
        return new DatasetDescription(
            Id: datasetDescription.Id,
            Name: datasetDescription.Name,
            UserId: datasetDescription.UserId);
    }
}