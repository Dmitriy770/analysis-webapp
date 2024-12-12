namespace Uis.Api.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication RegisterApi(this WebApplication app)
    {
        app.MapOpenApi();
        app.MapHealthChecks("/healthz");
        app.MapControllers();
        
        return app;
    }
}