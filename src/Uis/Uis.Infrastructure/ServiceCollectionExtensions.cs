using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using Uis.Application.Abstractions.Gateways;
using Uis.Application.Abstractions.Providers;
using Uis.Application.Commands;
using Uis.Common.HttpClient;
using Uis.Infrastructure.Gateways.GitHub;
using Uis.Infrastructure.Gateways.GitHub.Api;
using Uis.Infrastructure.Providers;
using Uis.Infrastructure.Repositories.Sessions;
using Uis.Infrastructure.Repositories.Users;
using Uis.Infrastructure.Settings;

namespace Uis.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssemblyContaining<LoginCommand>();
        });

        return serviceCollection;
    }
    
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection serviceCollection,
        IConfigurationRoot configuration)
    {
        // Providers
        serviceCollection.AddScoped<IDateTimeProvider, DateTimeProvider>();
        serviceCollection.AddScoped<IGuidProvider, GuidProvider>();
        
        // Gateways
        var settings = GitHubGatewaySettings.From(configuration);
        serviceCollection.AddSingleton(settings);
        
        serviceCollection
            .AddRefitClient<IGitHubApi>()
            .ConfigureHttpClient(httpClient => httpClient.BaseAddress = settings.ApiUri)
            .AddLogger<HttpClientLogger>();
        
        serviceCollection
            .AddRefitClient<IGitHubOAuthApi>()
            .ConfigureHttpClient(httpClient => httpClient.BaseAddress = settings.OauthUri)
            .AddLogger<HttpClientLogger>();
     
        serviceCollection.AddScoped<IGitHubGateway, GitHubGateway>();

        // repository
        serviceCollection.AddSessionRepository(configuration);
        serviceCollection.AddUserRepository(configuration);
        
        return serviceCollection;
    }
}