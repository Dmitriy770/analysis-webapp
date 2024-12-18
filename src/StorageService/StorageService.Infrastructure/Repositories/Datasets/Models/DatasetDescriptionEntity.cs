namespace StorageService.Infrastructure.Repositories.Datasets.Models;

internal sealed class DatasetDescriptionEntity
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public long UserId { get; set; }
}