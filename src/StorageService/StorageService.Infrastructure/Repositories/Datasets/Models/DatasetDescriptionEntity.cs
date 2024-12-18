using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace StorageService.Infrastructure.Repositories.Datasets.Models;

internal sealed class DatasetDescriptionEntity
{
    [BsonId]
    public ObjectId _id { get; set; }
    
    [BsonElement("id")]
    public Guid Id { get; set; }
    
    [BsonElement("name")]
    public string Name { get; set; }
    
    [BsonElement("userId")]
    public long UserId { get; set; }
}