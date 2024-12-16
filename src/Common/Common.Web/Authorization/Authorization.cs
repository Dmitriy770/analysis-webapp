using Common.Logging;
using Common.Logging.HttpClient;
using Common.Web.Authorization.Filters;
using Common.Web.Authorization.Filters.Api;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace Common.Web.Authorization;

public static class Authorization
{
    public static IServiceCollection AddAuthorizationFilter(this IServiceCollection services)
    {
        services.AddCommonLogging();
        services
            .AddRefitClient<IUserServiceClient>()
            .ConfigureHttpClient(httpClient => httpClient.BaseAddress = BaseAddress)
            .AddLogger<HttpClientLogger>();
        services.AddScoped<AuthorizationFilter>();
        
        return services;
    }

    public static IServiceCollection AddAuthorizationExceptionFilter(this IServiceCollection services)
    {
        services.AddScoped<AuthorizationExceptionFilter>();

        return services;
    }
    
    private static readonly Uri BaseAddress = new("http://uis-service.default.svc.cluster.local");
}