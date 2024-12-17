using Microsoft.Extensions.DependencyInjection;
using StorageService.Application.Abstractions.Providers;
using StorageService.Application.Abstractions.Repositories;
using StorageService.Infrastructure.Providers;
using StorageService.Infrastructure.Repositories.Datasets;
using StorageService.Infrastructure.Repositories.Files;

namespace StorageService.Infrastructure;

public static class InfrastructureService
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        // Repositories
        services.AddScoped<IFileRepository, FakeFileRepository>();
        services.AddScoped<IDatasetRepository, FakeDatasetRepository>();
        
        // Providers
        services.AddScoped<IGuidProvider, GuidProvider>();

        return services;
    }
}