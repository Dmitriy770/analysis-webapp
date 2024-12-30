using MediatR;
using StorageService.Application.Abstractions.Repositories;
using StorageService.Domain.Models;

namespace StorageService.Application.Queries;

public record GetDescriptionsByUserIdQuery(
    long UserId)
    : IStreamRequest<DatasetDescription>;
    
internal sealed class GetDescriptionsByUserIdQueryHandler(
    IDatasetRepository datasetRepository)
    : IStreamRequestHandler<GetDescriptionsByUserIdQuery, DatasetDescription>
{
    public IAsyncEnumerable<DatasetDescription> Handle(GetDescriptionsByUserIdQuery request, CancellationToken cancellationToken)
    {
        return datasetRepository.GetDescriptionsByUserIdAsync(request.UserId, cancellationToken);
    }
}