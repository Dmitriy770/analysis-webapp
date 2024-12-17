namespace StudyService.Application.Abstractions.Gateways.Models;

public sealed class DatasetDescription
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public long UserId { get; set; }
}