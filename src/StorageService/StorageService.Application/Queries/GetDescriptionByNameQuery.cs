using MediatR;
using StorageService.Application.Abstractions.Repositories;
using StorageService.Domain.Exceptions;
using StorageService.Domain.Models;

namespace StorageService.Application.Queries;

public record GetDescriptionByNameQuery(
    string Name,
    long UserId)
    : IRequest<DatasetDescription>;
    
internal sealed class GetDescriptionByNameQueryHandler(
    IDatasetRepository datasetRepository) : IRequestHandler<GetDescriptionByNameQuery, DatasetDescription>
{
    public async Task<DatasetDescription> Handle(GetDescriptionByNameQuery request, CancellationToken cancellationToken)
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