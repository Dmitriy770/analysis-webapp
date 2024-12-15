using Uis.Api.Filters.Internal;
using Uis.Api.Filters.Public;

namespace Uis.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApi(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<SessionControllerExceptionFilter>();
        serviceCollection.AddScoped<UserControllerExceptionFilter>();
        serviceCollection.AddScoped<AuthorizationExceptionFilter>();
        serviceCollection.AddScoped<AuthorizationFilter>();
        
        serviceCollection.AddOpenApi();
        serviceCollection.AddHealthChecks();
        
        serviceCollection.AddControllers(options =>
        {
            options.Filters.AddService<AuthorizationFilter>();
        });
        
        return serviceCollection;
    }
}