﻿using FluentMigrator.Runner;
using FluentMigrator.Runner.Processors.Postgres;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Npgsql.NameTranslation;
using Uis.Application.Abstractions.Repositories;
using Uis.Infrastructure.Repositories.Users.Migrations;
using Uis.Infrastructure.Repositories.Users.Models;
using Uis.Infrastructure.Settings;

namespace Uis.Infrastructure.Repositories.Users;

public static class SessionRepositoryConfiguration
{
    public static IServiceCollection AddUserRepository(
        this IServiceCollection serviceCollection,
        IConfigurationRoot configuration)
    {
        var settings = UserRepositorySettings.From(configuration);
        serviceCollection.AddSingleton(settings);
        
        ConfigureFluentMigrator(serviceCollection);

        serviceCollection.AddDbContext<UserRepositoryDbContext>();
        serviceCollection.AddScoped<IUserRepository, UserRepository>();
        
        return serviceCollection;
    }

    
    public static void RegisterUserRepository(this WebApplication application)
    {
        var runner = application.Services.GetRequiredService<IMigrationRunner>();
        runner.MigrateUp();
    }

    private static void ConfigureFluentMigrator(IServiceCollection serviceCollection)
    {
        serviceCollection.AddFluentMigratorCore()
            .ConfigureRunner(builder => builder
                .AddPostgres()
                .WithGlobalConnectionString(serviceProvider =>
                {
                    var settings = serviceProvider.GetRequiredService<UserRepositorySettings>();
                    return GetConnectionString(settings);
                })
                .ScanIn(typeof(AddUserTable).Assembly).For.Migrations())
            .AddLogging()
            .BuildServiceProvider(false);
    }
    
    private static string GetConnectionString(UserRepositorySettings settings)
    {
        var connectionStringBuilder = new NpgsqlConnectionStringBuilder
        {
            Host = settings.Endpoint,
            Port = settings.Port,
            Database = settings.Database,
            Username = settings.User,
            Password = settings.Password
        };
        
        return connectionStringBuilder.ConnectionString;
    }
}
