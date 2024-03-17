// <copyright file="DependencyInjection.cs" company="Tek">
// Copyright (c) TekChallenge.Services.ProductService. All rights reserved.
// </copyright>

using System.Text;
using Dapr.Client;
using Dapr.Extensions.Configuration;
using Google.Api;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using SharedDefinitions.Infrastructure.Services;
using TekChallenge.SharedDefinitions.Application.Abstractions.Services;
using TekChallenge.SharedDefinitions.Infrastructure.Abstractions;
using TekChallenge.SharedDefinitions.Infrastructure.Authentication;
using TekChallenge.SharedDefinitions.Infrastructure.Services;

namespace TekChallenge.SharedDefinitions.Infrastructure;

/// <summary>
/// Dependency Injection.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Add Infrastructure dependencies.
    /// </summary>
    /// <param name="builder">WebApplicationBuilder being extended.</param>
    /// <param name="serviceName">The name of this running service.</param>
    /// <returns>Services with dependencies injected.</returns>
    public static IServiceCollection AddSharedDefinitionsInfrastructure(this WebApplicationBuilder builder, string serviceName = "")
    {
        builder
            .AddLoggingAndTracing(serviceName);

        builder.Services
             .AddSharedDefinitionsPersistance(builder.Configuration)
             .AddSharedDefinitionsAuthentication(builder.Configuration);

        builder.Services.AddDaprClient();

        builder.Services.AddScoped<IExceptionHandlingService, ExceptionHandlingService>();

        builder.Services.AddScoped<ICacheService, DaprCacheService>();

        return builder.Services;
    }

    private static void AddLoggingAndTracing(this WebApplicationBuilder builder, string serviceName)
    {
        var connectionStringEncoded = "SW5zdHJ1bWVudGF0aW9uS2V5PWEzMzNiMzJjLWMyZWEtNGRkNi05MGQ4LWNlN2JmZGUyNmZjMTtJbmdlc3Rpb25FbmRwb2ludD1odHRwczovL2Vhc3R1cy04LmluLmFwcGxpY2F0aW9uaW5zaWdodHMuYXp1cmUuY29tLztMaXZlRW5kcG9pbnQ9aHR0cHM6Ly9lYXN0dXMubGl2ZWRpYWdub3N0aWNzLm1vbml0b3IuYXp1cmUuY29tLw ==";
        var telemetryConfiguration = TelemetryConfiguration.CreateFromConfiguration(Encoding.UTF8.GetString(Convert.FromBase64String(connectionStringEncoded)));
        builder.Services.AddApplicationInsightsTelemetry((serviceOptions) =>
        {
            serviceOptions.ConnectionString = telemetryConfiguration.ConnectionString;
        });

        builder.Host.UseSerilog((chostBuilderContextontext, services, loggerConfiguration) =>
        {
            loggerConfiguration
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
                .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .Enrich.WithProperty("Application", serviceName)
                .WriteTo.Async(a => a.Console(outputTemplate: "{Application}: [{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}", theme: AnsiConsoleTheme.Code));

            // Adding Seq sink
            loggerConfiguration.WriteTo.Seq("http://seq");

            // Adding File sink
            loggerConfiguration.WriteTo.File("logs/access-log-.txt", rollingInterval: RollingInterval.Day, retainedFileCountLimit: null);

            // Adding Application Insights sink
            loggerConfiguration.WriteTo.ApplicationInsights(telemetryConfiguration, TelemetryConverter.Traces);
        });
    }

    /// <summary>
    /// Add Persistence dependencies.
    /// </summary>
    /// <param name="services">Injected services.</param>
    /// <param name="configuration">Injected _configuration.</param>
    /// <returns>Services with dependencies injected.</returns>
    private static IServiceCollection AddSharedDefinitionsPersistance(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IPostgresSqlConnectionFactory, PostgresSqlConnectionFactory>();

        return services;
    }

    /// <summary>
    /// Add Authentication dependencies.
    /// </summary>
    /// <param name="services">Injected services.</param>
    /// <param name="configuration">Injected _configuration.</param>
    /// <returns>Services with dependencies injected.</returns>
    private static IServiceCollection AddSharedDefinitionsAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = new JwtSettings
        {
            Secret = "my-super-secret-key@!12345678901",
            ExpireDays = 1,
            Issuer = "TekChallenge",
            Audience = "TekChallenge",
        };

        services.AddSingleton(Options.Create(jwtSettings));

        services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
            });

        return services;
    }
}
