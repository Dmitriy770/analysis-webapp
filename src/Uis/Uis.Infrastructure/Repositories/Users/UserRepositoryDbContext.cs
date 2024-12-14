using Microsoft.EntityFrameworkCore;
using Npgsql;
using Uis.Infrastructure.Repositories.Users.Models;
using Uis.Infrastructure.Settings;

namespace Uis.Infrastructure.Repositories.Users;

internal class UserRepositoryDbContext(
    UserRepositorySettings settings)
    : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionStringBuilder = new NpgsqlConnectionStringBuilder
        {
            Host = settings.Endpoint,
            Port = settings.Port,
            Database = settings.Database,
            Username = settings.User,
            Password = settings.Password
        };
        
        optionsBuilder.UseNpgsql(connectionStringBuilder.ConnectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserStorageRecord>(entityBuilder =>
        {
            entityBuilder
                .ToTable("Users")
                .HasKey(user => user.Id);
            
            entityBuilder
                .Property(user => user.Id).HasColumnName("UserId");
            
            entityBuilder
                .Property(user => user.Login)
                .HasColumnName("Login")
                .HasMaxLength(40);
            
            entityBuilder
                .Property(user => user.Name)
                .HasColumnName("Name")
                .HasMaxLength(40);
            
            entityBuilder
                .Property(user => user.AvatarUri)
                .HasColumnName("AvatarUri");
            
            entityBuilder
                .Property(user => user.Limit)
                .HasColumnName("Limit");
        });
    }
    
    public DbSet<UserStorageRecord> Users { get; set; }
}