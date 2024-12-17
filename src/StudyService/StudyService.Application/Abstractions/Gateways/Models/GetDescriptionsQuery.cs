namespace StudyService.Application.Abstractions.Gateways.Models;

public sealed class GetDescriptionsQuery
{
    public long UserId { get; set; }
    
    public string DatasetName { get; set; }
}