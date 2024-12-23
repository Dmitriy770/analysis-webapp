﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using MongoDB.Driver.GridFS;
using StorageService.Application.Abstractions.Providers;
using StorageService.Application.Abstractions.Repositories;
using StorageService.Infrastructure.Providers;
using StorageService.Infrastructure.Repositories.Datasets;
using StorageService.Infrastructure.Repositories.Files;
using StorageService.Infrastructure.Settings;

namespace StorageService.Infrastructure;

public static class InfrastructureService
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfigurationRoot configuration)
    {
        // Repositories
        // services.AddMongo(configuration);
        // services.AddScoped<IFileRepository, FileRepository>();
        // services.AddDbContext<DatasetRepositoryDbContext>();
        // services.AddScoped<IDatasetRepository, DatasetRepository>();
        services.AddScoped<IFileRepository, FakeFileRepository>();
        services.AddScoped<IDatasetRepository, FakeDatasetRepository>();
        
        // Providers
        services.AddScoped<IGuidProvider, GuidProvider>();

        return services;
    }

    private static IServiceCollection AddMongo(this IServiceCollection services, IConfigurationRoot configuration)
    {
        var settings = MongoSettings.From(configuration);
        services.AddSingleton(settings);
        
        var mongoClientSettings = new MongoClientSettings
        {
            Scheme = ConnectionStringScheme.MongoDB,
            Server = new MongoServerAddress(settings.Host, settings.Port),
            Credential = MongoCredential.CreateCredential(settings.Database, settings.Username, settings.Password),
            
        };
        Console.WriteLine($"MongoDB server: {settings.Host}:{settings.Port}, {settings.Username}, {settings.Password}");
        var mongoClient = new MongoClient($"mongodb://{settings.Username}:{settings.Password}@{settings.Host}:{settings.Port}/{settings.Database}?authSource=admin");
        var database = mongoClient.GetDatabase(settings.Database);
       
        services.AddSingleton<IMongoClient>(database.Client);
        services.AddSingleton(database);

        var options = new GridFSBucketOptions
        {
            BucketName = settings.BucketName,
        };
        var bucket = new GridFSBucket(database, options);
        services.AddSingleton<IGridFSBucket>(bucket);
        
        return services;
    }
}