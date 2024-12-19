using System.Net;
using MediatR;
using StudyService.Application.Abstractions.Gateways;
using StudyService.Application.Abstractions.Gateways.Models;
using StudyService.Application.Abstractions.Producers;
using StudyService.Application.Abstractions.Providers;
using StudyService.Application.Abstractions.Repositories;
using StudyService.Domain.Exceptions;
using StudyService.Domain.Models;
using StudyService.Domain.Models.Studies;
using StudyService.Domain.Models.StudyDatasets;

namespace StudyService.Application.Commands;

public record CreateClusteringStudyCommand(
    NewClusteringStudy NewStudy,
    long UserId)
    : IRequest<Study>;

internal sealed class CreateClusteringStudyCommandHandler(
    IStudyRepository studyRepository,
    IStorageServiceGateway storageServiceGateway,
    IStudyProducer studyProducer,
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

        var studyDataset = new StudyDataset(
            Id: description.Id,
            Name: description.Name,
            Columns: newStudy.Dataset.Columns);
        
        var studyId = guidProvider.NewGuid();
        var study = new Study(
            Id: studyId,
            UserId: userId,
            Type: StudyType.Clustering,
            Status: StudyStatus.InProgress,
            Components: newStudy.Components,
            CreationDate: dateTimeProvider.Now,
            Dataset: studyDataset);
        
        await studyRepository.AddAsync(study, cancellationToken);
        
        await studyProducer.ProduceAsync(study, cancellationToken);
        
        return study;
    }

    private async Task<DatasetDescription?> GeDescription(long userId, string datasetName)
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