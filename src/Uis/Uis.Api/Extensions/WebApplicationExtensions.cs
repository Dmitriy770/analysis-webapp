using Scalar.AspNetCore;

namespace Uis.Api.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication RegisterApi(this WebApplication app)
    {
        app.MapOpenApi("openapi/v1");
        app.MapScalarApiReference(options =>
        {
            options.WithOpenApiRoutePattern("/openapi/{documentName}");
        });
        
        app.MapHealthChecks("/healthz");
        app.MapControllers();
        
        return app;
    }
}