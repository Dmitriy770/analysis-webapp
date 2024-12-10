﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using Uis.Infrastructure.Gateways.Abstractions;
using Uis.Infrastructure.Settings;

namespace Uis.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection serviceCollection,
        IConfigurationRoot configuration)
    {
        // Gateways
        var settings = GitHubGatewaySettings.From(configuration);
        serviceCollection.AddSingleton(settings);
        serviceCollection
            .AddRefitClient<IGitHubGateway>()
            .ConfigureHttpClient(httpClient => httpClient.BaseAddress = settings.ApiUri);
        
        return serviceCollection;
    }
}