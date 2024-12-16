using Common.Logging.HttpClient;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Logging;

public static class Logging
{
    public static IServiceCollection AddCommonLogging(this IServiceCollection services)
    {
        services.AddScoped<HttpClientLogger>();
        
        return services;
    }
}