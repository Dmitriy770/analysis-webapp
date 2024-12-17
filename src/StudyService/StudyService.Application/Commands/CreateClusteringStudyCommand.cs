using System.Net;
using MediatR;
using StudyService.Application.Abstractions.Gateways;
using StudyService.Application.Abstractions.Gateways.Models;
using StudyService.Application.Abstractions.Providers;
using StudyService.Domain.Exceptions;
using StudyService.Domain.Models;

namespace StudyService.Application.Commands;

public record CreateClusteringStudyCommand(
    NewClusteringStudy NewStudy,
    long UserId)
    : IRequest<Study>;

internal sealed class CreateClusteringStudyCommandHandler(
    IStorageServiceGateway storageServiceGateway,
    IGuidProvider guidProvider,
    IDateTimeProvider dateTimeProvider)
    : IRequestHandler<CreateClusteringStudyCommand, Study>
{
    
    public async Task<Study> Handle(CreateClusteringStudyCommand request, CancellationToken cancellationToken)
    {
        var (newStudy, userId) = request;
        if (await GeDescription(userId, newStudy.Dataset.Name) is not { } description)
        {
            throw new DatasetNotFoundException(newStudy.Dataset.Name);
        }

        var studyId = guidProvider.NewGuid();
        var study = new Study(
            Id: studyId,
            Type: StudyType.Clustering,
            Status: StudyStatus.InProgress,
            Components: newStudy.Components,
            CreationDate: dateTimeProvider.Now 
            )
    }

    private async Task<string?> GeDescription(long userId, string datasetName)
    { 
        var queryParams = new GetDescriptionsQuery
        {
            DatasetName = datasetName,
            UserId = userId
        };
        
        try
        {
            return await storageServiceGateway.GetDatasetDescription(queryParams);
        }
        catch (Refit.ApiException exception) when(exception.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }
    }
}