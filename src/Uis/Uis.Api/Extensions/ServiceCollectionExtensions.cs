using Uis.Api.Filters;
using Uis.Api.Filters.Public;

namespace Uis.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddControllers(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddOpenApi();
        
        serviceCollection.AddControllers(options =>
        {
            options.Filters.AddService<AuthorizationFilter>();
        });
        
        return serviceCollection;
    }
}