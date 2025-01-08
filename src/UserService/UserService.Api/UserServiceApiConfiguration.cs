using Npgsql;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Scalar.AspNetCore;
using Uis.Api.Filters.Internal;
using Uis.Api.Filters.Public;
using MediatR.OpenTelemetry;
using Uis.Application.Commands;
using Uis.Application.Queries;

namespace Uis.Api;

public static class UserServiceApiConfiguration
{
    public static WebApplicationBuilder AddApi(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<SessionControllerExceptionFilter>();
        builder.Services.AddScoped<UserControllerExceptionFilter>();
        builder.Services.AddScoped<AuthorizationFilter>();
        
        builder.Services.AddOpenApi();
        builder.Services.AddHealthChecks();
        
        builder.Services.AddControllers(options =>
        {
            options.Filters.AddService<AuthorizationFilter>();
        });
        
        return builder;
    }

    public static WebApplication UseApi(this WebApplication app)
    {
        app.MapOpenApi("openapi/v1");
        app.MapScalarApiReference(options =>
        {
            options.WithOpenApiRoutePattern("/api/uis/openapi/{documentName}");
        });
        
        app.MapHealthChecks("/healthz");
        app.MapControllers();

        return app;
    }

    public static WebApplicationBuilder AddOpenTelemetry(this WebApplicationBuilder builder)
    {
        AddMediatrTelemetry(builder);
        
        builder.Services.AddOpenTelemetry()
            .ConfigureResource(resource => resource.AddService("UserService"))
            .WithMetrics(metrics =>
            {
                metrics
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddNpgsqlInstrumentation();
                
                metrics.AddConsoleExporter();
            })
            .WithTracing(tracing =>
            {
                tracing
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddEntityFrameworkCoreInstrumentation()
                    .AddNpgsql();

                tracing.AddConsoleExporter();
            });

        builder.Logging.AddOpenTelemetry(logging => logging.AddConsoleExporter());

        return builder;
    }

    private static void AddMediatrTelemetry(WebApplicationBuilder builder)
    {
        builder.Services.AddMediatRTelemetry(configuration =>
        {
            configuration.Add<LoginCommand>();
            configuration.Add<LogoutCommand>();
            configuration.Add<ValidateSession>();
            configuration.Add<GetUserBySessionIdQuery>();
            configuration.Add<GetUserByUserIdQuery>();
        });
    }
}