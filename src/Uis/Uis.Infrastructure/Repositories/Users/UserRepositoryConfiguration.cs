using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Uis.Application.Abstractions.Repositories;
using Uis.Infrastructure.Settings;

namespace Uis.Infrastructure.Repositories.Users;

public static class SessionRepositoryConfiguration
{
    public static IServiceCollection AddUserRepository(
        this IServiceCollection serviceCollection,
        IConfigurationRoot configuration)
    {
        var settings = UserRepositorySettings.From(configuration);
        serviceCollection.AddSingleton(settings);

        serviceCollection.AddDbContext<UserRepositoryDbContext>();
        
        serviceCollection.AddScoped<IUserRepository, UserRepository>();
        
        return serviceCollection;
    }

    public static async Task RegisterUserRepository(this WebApplication application)
    {
        var dbContext = application.Services.GetRequiredService<UserRepositoryDbContext>();
        
        await dbContext.Database.MigrateAsync();
    }
}
