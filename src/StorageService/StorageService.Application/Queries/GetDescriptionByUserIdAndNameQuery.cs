using MediatR;
using StorageService.Application.Abstractions.Repositories;
using StorageService.Domain.Exceptions;
using StorageService.Domain.Models;

namespace StorageService.Application.Queries;

public record GetDescriptionByUserIdAndNameQuery(
    string Name,
    long UserId)
    : IRequest<DatasetDescription>;
    
internal sealed class GetDescriptionByNameQueryHandler(
    IDatasetRepository datasetRepository) : IRequestHandler<GetDescriptionByUserIdAndNameQuery, DatasetDescription>
{
    public async Task<DatasetDescription> Handle(GetDescriptionByUserIdAndNameQuery request, CancellationToken cancellationToken)
    {
        var description = await datasetRepository
            .GetDescriptionsByUserIdAsync(request.UserId, cancellationToken)
            .FirstOrDefaultAsync(dataset => dataset.Name == request.Name, cancellationToken);
        if (description is null)
        {
            throw new DatasetNotFoundException(request.Name);
        }
        
        return description;
    }
}