using Common.Logging.HttpClient;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Common.Logging;

public static class Logging
{
    public static IServiceCollection AddCommonLogging(this IServiceCollection services)
    {
        services.TryAddScoped<HttpClientLogger>();
        
        return services;
    }
}