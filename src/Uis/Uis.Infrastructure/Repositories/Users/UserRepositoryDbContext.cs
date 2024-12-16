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
                .ToTable("users")
                .HasKey(user => user.Id);
            
            entityBuilder
                .Property(user => user.Id).HasColumnName("id");
            
            entityBuilder
                .Property(user => user.Login)
                .HasColumnName("login")
                .HasMaxLength(40);
            
            entityBuilder
                .Property(user => user.Name)
                .HasColumnName("name")
                .HasMaxLength(40);
            
            entityBuilder
                .Property(user => user.AvatarUri)
                .HasColumnName("avatar_uri")
                .HasMaxLength(100);
            
            entityBuilder
                .Property(user => user.Limit)
                .HasColumnName("limit");
        });
    }
    
    public DbSet<UserStorageRecord> Users { get; set; }
}