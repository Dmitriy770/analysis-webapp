using StorageService.Api.Models;

using DomainModels = StorageService.Domain.Models;

namespace StorageService.Api.Mappers;

internal static class DatasetDescriptionMapper
{
    internal static DatasetDescription ToApi(this DomainModels.DatasetDescription description)
    {
        return new DatasetDescription(
            Id: description.Id,
            Name: description.Name,
            UserId: description.UserId);
    }
}