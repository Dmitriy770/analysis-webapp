using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using StorageService.Domain.Models;
using StorageService.Infrastructure.Repositories.Datasets.Models;
using StorageService.Infrastructure.Settings;

namespace StorageService.Infrastructure.Repositories.Datasets;

internal sealed class DatasetRepositoryDbContext(
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
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<DatasetRepositoryDbContext>();
    }
    
    public DbSet<DatasetDescriptionEntity> DatasetDescriptions { get; set; }
}