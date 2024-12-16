using MediatR;
using StorageService.Application.Abstractions.Repositories;
using StorageService.Domain.Exceptions;
using StorageService.Domain.Models;

namespace StorageService.Application.Queries;

public record GetDescriptionByIdQuery(
    Guid Id)
    : IRequest<DatasetDescription>;
    
internal sealed class GetDescriptionByIdQueryHandler(
    IDatasetRepository datasetRepository
    )
    : IRequestHandler<GetDescriptionByIdQuery, DatasetDescription>
{
    public async Task<DatasetDescription> Handle(GetDescriptionByIdQuery request, CancellationToken cancellationToken)
    {
        if (await datasetRepository.GetDescriptionAsync(request.Id, cancellationToken) is not { } description)
        {
            throw new DatasetNotFoundException(request.Id.ToString());
        }
        
        return description;
    }
}