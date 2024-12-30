using MediatR;
using StorageService.Application.Abstractions.Repositories;
using StorageService.Domain.Exceptions;

namespace StorageService.Application.Queries;

public record GetContentByIdQuery(
    Guid Id)
    : IRequest<Result>;

public record Result(
   string Name,
   Stream Content);
   
internal sealed class GetContentByIdQueryHandler(
    IDatasetRepository datasetRepository,
    IFileRepository fileRepository)
    : IRequestHandler<GetContentByIdQuery, Result>
{
    public async Task<Result> Handle(GetContentByIdQuery request, CancellationToken cancellationToken)
    {
        if (await datasetRepository.GetDescriptionAsync(request.Id, cancellationToken) is not { } description)
        {
            throw new DatasetNotFoundException(request.Id.ToString());
        }

        if (await fileRepository.DownloadAsync(description.FileName, cancellationToken) is not { } fileStream)
        {
            throw new DatasetNotFoundException(request.Id.ToString());
        }

        var result = new Result(
            Name: $"{description.Name}.csv",
            Content: fileStream);
        return result;
    }
}