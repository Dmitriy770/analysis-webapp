using Refit;

namespace StudyService.Application.Abstractions.Gateways.Models;

public sealed class GetDescriptionsQuery
{
    [AliasAs("userId")]
    public long UserId { get; set; }
    
    [AliasAs("datasetName")]
    public string DatasetName { get; set; }
}