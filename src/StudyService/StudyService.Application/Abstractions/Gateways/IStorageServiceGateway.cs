using Refit;
using StudyService.Application.Abstractions.Gateways.Models;

namespace StudyService.Application.Abstractions.Gateways;

public interface IStorageServiceGateway
{
    [Get("/internal/datasets/description")]
    public Task<DatasetDescription> GetDatasetDescription([Query] GetDescriptionsQuery query);
}