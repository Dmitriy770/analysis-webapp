using Uis.Api.Filters.Internal;
using Uis.Api.Filters.Public;

namespace Uis.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApi(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddOpenApi();
        serviceCollection.AddHealthChecks();
        
        serviceCollection.AddControllers(options =>
        {
            options.Filters.AddService<AuthorizationFilter>();
        });

        serviceCollection.AddScoped<SessionControllerExceptionFilter>();
        serviceCollection.AddScoped<UserControllerExceptionFilter>();
        
        return serviceCollection;
    }
}