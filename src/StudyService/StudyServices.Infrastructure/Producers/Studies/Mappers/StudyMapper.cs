using StudyService.Domain.Models.Studies;
using StudyService.Domain.Models.StudyDatasets;
using StudyServices.Infrastructure.Producers.Studies.Models;

namespace StudyServices.Infrastructure.Producers.Studies.Mappers;

internal static class StudyMapper
{
    internal static StudyEntity ToInfrastructure(this Study study)
    {
        return new StudyEntity(
            Id: study.Id,
            Components: study.Components,
            DatasetEntity: study.Dataset.ToInfrastructure());
    }
    

    private static StudyDatasetEntity ToInfrastructure(this StudyDataset dataset)
    {
        return new StudyDatasetEntity(
            Id: dataset.Id,
            Name: dataset.Name,
            Columns: dataset.Columns);
    }
}