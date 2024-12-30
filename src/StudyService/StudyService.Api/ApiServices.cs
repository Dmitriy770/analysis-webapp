using Common.Web.Authorization;
using Common.Web.Authorization.Extensions;
using StudyService.Api.ExceptionFilters;

namespace StudyService.Api;

public static class ApiServices
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        // Filters
        services.AddSingleton<InternalStudyControllerExceptionFilter>();
        services.AddSingleton<StudyControllerExceptionFilter>();
        
        // Authorization
        services.AddAuthorizationFilter();
        
        // Controllers
        services.AddOpenApi();
        services.AddHealthChecks();
        services.AddControllers(options =>
        {
            options.AddAuthorizationFilter();
        });

        return services;
    }

    public static WebApplication MapApiServices(this WebApplication app)
    {
        app.MapOpenApi();
        app.MapHealthChecks("/healthz");
        app.MapControllers();

        return app;
    }
}