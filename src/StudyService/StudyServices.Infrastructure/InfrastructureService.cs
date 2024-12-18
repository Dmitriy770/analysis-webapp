using Common.Logging.HttpClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using StudyService.Application.Abstractions.Gateways;
using StudyService.Application.Abstractions.Producers;
using StudyService.Application.Abstractions.Providers;
using StudyService.Application.Abstractions.Repositories;
using StudyServices.Infrastructure.Common.Kafka;
using StudyServices.Infrastructure.Producers.Studies;
using StudyServices.Infrastructure.Producers.Studies.Models;
using StudyServices.Infrastructure.Providers;
using StudyServices.Infrastructure.Repositories.Studies;
using StudyServices.Infrastructure.Repositories.StudyResults;
using StudyServices.Infrastructure.Settings;

namespace StudyServices.Infrastructure;

public static class InfrastructureService
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfigurationRoot configuration)
    {
        // Gateways
        services
            .AddRefitClient<IUserServiceGateway>()
            .ConfigureHttpClient(httpClient => httpClient.BaseAddress = UserServiceAddress)
            .AddLogger<HttpClientLogger>();
        services
            .AddRefitClient<IStorageServiceGateway>()
            .ConfigureHttpClient(httpClient => httpClient.BaseAddress = StorageServiceAddress)
            .AddLogger<HttpClientLogger>();
        
        // Repositories
        services.AddScoped<IStudyRepository, FakeStudyRepository>();
        services.AddScoped<IStudyResultRepository, FakeStudyResultRepository>();
        
        // Providers
        services.AddScoped<IGuidProvider, GuidProvider>();
        services.AddScoped<IDateTimeProvider, DateTimeProvider>();
        
        // Producers
        services.AddKafkaProducer(configuration);
        services.AddScoped<IStudyProducer, StudyProducer>();

        return services;
    }

    private static IServiceCollection AddKafkaProducer(
        this IServiceCollection services,
        IConfigurationRoot configuration)
    {
        var settings = StudyProducerSettings.From(configuration);
        services.AddSingleton(settings);
        
        services.AddSingleton<KafkaClientHandle>();
        services.AddSingleton<KafkaDependentProducer<string, string>>();
            
        return services;
    }

    private static readonly Uri UserServiceAddress = new("http://uis-service.default.svc.cluster.local");
    private static readonly Uri StorageServiceAddress = new("http://storage-service.default.svc.cluster.local");
}
