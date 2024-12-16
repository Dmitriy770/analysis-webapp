using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using Uis.Application.Abstractions.Repositories;
using Uis.Infrastructure.Settings;

namespace Uis.Infrastructure.Repositories.Sessions;

internal static class SessionRepositoryConfiguration
{
    public static IServiceCollection AddSessionRepository(
        this IServiceCollection serviceCollection,
        IConfigurationRoot configuration)
    {
        var redisSettings = SessionRepositorySettings.From(configuration);
        var redisConfiguration = new ConfigurationOptions
        {
            EndPoints = {{redisSettings.Endpoint, redisSettings.Port}},
            User = redisSettings.User,
            Password = redisSettings.Password,
            AbortOnConnectFail = false
        };
        
        var redis = ConnectionMultiplexer.Connect(redisConfiguration);
        var database = redis.GetDatabase();
        
        serviceCollection.AddSingleton(database);
        serviceCollection.AddScoped<ISessionRepository, SessionRepository>();
        
        return serviceCollection;
    }
}