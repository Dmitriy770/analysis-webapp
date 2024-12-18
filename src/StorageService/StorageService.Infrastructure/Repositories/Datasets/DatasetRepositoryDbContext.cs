using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoDB.EntityFrameworkCore.Extensions;
using StorageService.Domain.Models;
using StorageService.Infrastructure.Settings;

namespace StorageService.Infrastructure.Repositories.Datasets;

internal class DatasetRepositoryDbContext(
    IMongoClient mongoClient,
    MongoSettings settings)
    : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMongoDB(mongoClient, settings.Database);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DatasetRepositoryDbContext>().ToCollection("datasetDescriptions");
    }
    
    public DbSet<DatasetDescription> DatasetDescriptions { get; set; }
}