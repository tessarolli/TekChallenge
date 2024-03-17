// <copyright file="DependencyInjection.cs" company="Tek">
// Copyright (c) TekChallenge.Services.ProductService. All rights reserved.
// </copyright>

using System.Text;
using Dapr.Client;
using Dapr.Extensions.Configuration;
using Google.Api;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Serilog;
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
        builder.Host.UseSerilog((ctx, lc) =>
           lc.ReadFrom
               .Configuration(ctx.Configuration)
               .Enrich.WithProperty("Application", serviceName));

        builder.Services
             .AddSharedDefinitionsPersistance(builder.Configuration)
             .AddSharedDefinitionsAuthentication(builder.Configuration);

        builder.Services.AddDaprClient();

        builder.Services.AddScoped<IExceptionHandlingService, ExceptionHandlingService>();

        builder.Services.AddScoped<ICacheService, DaprCacheService>();

        return builder.Services;
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
