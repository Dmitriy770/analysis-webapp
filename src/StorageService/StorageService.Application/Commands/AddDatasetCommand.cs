using MediatR;
using StorageService.Application.Abstractions.Providers;
using StorageService.Application.Abstractions.Repositories;
using StorageService.Domain.Exceptions;
using StorageService.Domain.Models;

namespace StorageService.Application.Commands;

public record AddDatasetCommand(
    string FileName,
    long UserId,
    byte[] Content)
    : IRequest<DatasetDescription>;
    
internal sealed class AddDatasetCommandHandler(
    IDatasetRepository datasetRepository,
    IFileRepository fileRepository,
    IGuidProvider guidProvider)
    : IRequestHandler<AddDatasetCommand, DatasetDescription>
{
    public async Task<DatasetDescription> Handle(AddDatasetCommand request, CancellationToken cancellationToken)
    {
        var datasetName = request.FileName.Replace(".csv", string.Empty);
        var datasetId = guidProvider.NewGuid();
        
        var description = new DatasetDescription(
            Id: datasetId,
            Name: datasetName,
            request.UserId);
        var descriptions = await datasetRepository
            .GetDescriptionsByUserIdAsync(request.UserId, cancellationToken)
            .FirstOrDefaultAsync(dataset => dataset.Name == datasetName, cancellationToken);
        if (descriptions is not null)
        {
            throw new DatasetDuplicateException(datasetName);
        }
        await datasetRepository.AddDescriptionAsync(description, cancellationToken);
        
        await fileRepository.UploadAsync(description.FileName, request.Content, cancellationToken);

        return description;
    }
}