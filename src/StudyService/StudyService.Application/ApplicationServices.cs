using Microsoft.Extensions.DependencyInjection;

namespace StudyService.Application;

public static class ApplicationServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(typeof(ApplicationServices).Assembly);
        });

        return services;
    }
}