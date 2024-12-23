﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.EntityFrameworkCore;

namespace StorageService.Infrastructure.Repositories.Datasets.Models;

[Collection("datasetDescriptions")]
public sealed class DatasetDescriptionEntity
{
    [BsonId(IdGenerator = typeof(ObjectIdGenerator))]
    public ObjectId _id { get; set; }
    
    [BsonElement("id")]
    public Guid Id { get; set; }
    
    [BsonElement("name")]
    public string Name { get; set; }
    
    [BsonElement("userId")]
    public long UserId { get; set; }
}