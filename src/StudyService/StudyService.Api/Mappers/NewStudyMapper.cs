using StudyService.Api.Models.Studies;
using StudyService.Api.Models.StudyDatasets;
using StudyService.Domain.Models.Studies;


namespace StudyService.Api.Mappers;

internal static class NewStudyMapper
{
    public static NewClusteringStudy ToDomain(this NewStudy study)
    {
        return new NewClusteringStudy(
            Components: study.Components,
            Dataset: study.Dataset.ToDomain());
    }

    private static Domain.Models.StudyDatasets.NewStudyDataset ToDomain(this NewStudyDataset dataset)
    {
        return new Domain.Models.StudyDatasets.NewStudyDataset(
            Name: dataset.Name,
            Columns: dataset.Columns);
    }
}