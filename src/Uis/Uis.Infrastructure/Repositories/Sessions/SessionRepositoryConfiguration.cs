using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NRedisStack.RedisStackCommands;
using NRedisStack.Search;
using NRedisStack.Search.Literals.Enums;
using StackExchange.Redis;
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
            EndPoints = {$"{redisSettings.Endpoint}:{redisSettings.Port}"},
            User = redisSettings.User,
            Password = redisSettings.Password,
            AbortOnConnectFail = false
        };
        
        var redis = ConnectionMultiplexer.Connect(redisConfiguration);
        var database = redis.GetDatabase();
        CreateIndex(database);
        
        serviceCollection.AddSingleton(database);
        
        return serviceCollection;
    }

    private static void CreateIndex(IDatabase database)
    {
        var schema = new Schema()
            .AddTextField(new FieldName("$.SessionId", "SessionId"))
            .AddNumericField(new FieldName("$.UserId", "UserId"))
            .AddTextField(new FieldName("$.CreatedDateTime", "CreatedDateTime"));
        database.FT().Create(
            "idx:session",
            new FTCreateParams()
                .On(IndexDataType.JSON)
                .Prefix("session:"),
            schema);
    }
}