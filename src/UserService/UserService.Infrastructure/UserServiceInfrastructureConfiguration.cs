using Common.Logging.HttpClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using Uis.Application.Abstractions.Gateways;
using Uis.Application.Abstractions.Providers;
using Uis.Infrastructure.Gateways.GitHub;
using Uis.Infrastructure.Gateways.GitHub.Api;
using Uis.Infrastructure.Providers;
using Uis.Infrastructure.Repositories.Sessions;
using Uis.Infrastructure.Repositories.Users;
using Uis.Infrastructure.Settings;

namespace Uis.Infrastructure;

public static class UserServiceInfrastructureConfiguration
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfigurationRoot configuration)
    {
        AddProviders(services);
        AddGateways(services, configuration);
        AddRepositories(services, configuration);
        return services;
    }

    private static void AddProviders(IServiceCollection services)
    {
        services.AddScoped<IDateTimeProvider, DateTimeProvider>();
        services.AddScoped<IGuidProvider, GuidProvider>();
    }

    private static void AddGateways(IServiceCollection services, IConfigurationRoot configuration)
    {
        var settings = GitHubGatewaySettings.From(configuration);
        services.AddSingleton(settings);
        
        services
            .AddRefitClient<IGitHubApi>()
            .ConfigureHttpClient(httpClient => httpClient.BaseAddress = settings.ApiUri)
            .AddLogger<HttpClientLogger>();
        
        services
            .AddRefitClient<IGitHubOAuthApi>()
            .ConfigureHttpClient(httpClient => httpClient.BaseAddress = settings.OauthUri)
            .AddLogger<HttpClientLogger>();
     
        services.AddScoped<IGitHubGateway, GitHubGateway>();
    }

    private static void AddRepositories(IServiceCollection services, IConfigurationRoot configuration)
    {
        services.AddSessionRepository(configuration);
        services.AddUserRepository(configuration);
    }
}