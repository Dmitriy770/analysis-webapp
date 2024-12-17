using StudyService.Api.Models.StudyDatasets;
using StudyService.Domain.Models.Studies;

using Study = StudyService.Api.Models.Studies.Study;

namespace StudyService.Api.Mappers;

public static class StudyMapper
{
    public static Study ToApi(this Domain.Models.Studies.Study study)
    {
        return new Study(
            Id: study.Id,
            Components: study.Components,
            Type: study.Type.ToApi(),
            Status: study.Status.ToApi(),
            CreationDate: study.CreationDate,
            Dataset: study.Dataset.ToApi());
    }

    private static string ToApi(this StudyType type)
    {
        return type switch
        {
            StudyType.Clustering => "clustering"
        };
    }
    
    private static string ToApi(this StudyStatus status)
    {
        return status switch
        {
            StudyStatus.InProgress => "in-progress",
            StudyStatus.Done => "in-done"
        };
    }

    private static StudyDataset ToApi(this Domain.Models.StudyDatasets.StudyDataset dataset)
    {
        return new StudyDataset(
            Id: dataset.Id,
            Name: dataset.Name,
            Columns: dataset.Columns);
    }
}