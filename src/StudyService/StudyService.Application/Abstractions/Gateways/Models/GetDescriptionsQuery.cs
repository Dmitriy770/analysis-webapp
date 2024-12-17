using Refit;

namespace StudyService.Application.Abstractions.Gateways.Models;

public sealed class GetDescriptionsQuery
{
    [AliasAs("userId")]
    public long UserId { get; set; }
    
    [AliasAs("datastName")]
    public string DatasetName { get; set; }
}