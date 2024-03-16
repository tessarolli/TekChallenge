// <copyright file="DependencyInjection.cs" company="Tek">
// Copyright (c) AuthService. All rights reserved.
// </copyright>

using System.Reflection;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Mapster;
using MapsterMapper;
using TekChallenge.Services.AuthService.API.Swagger;
using TekChallenge.Services.AuthService.Application.Abstractions.Repositories;
using TekChallenge.Services.AuthService.Infrastructure.Repositories;
using TekChallenge.SharedDefinitions.Domain.Common.Abstractions;
using TekChallenge.SharedDefinitions.Infrastructure.Services;
using TekChallenge.Services.AuthService.Application.Abstractions.Authentication;
using TekChallenge.Services.AuthService.Infrastructure.Authentication;

namespace TekChallenge.Services.AuthService.API;

/// <summary>
/// Dependency Injection.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Register local resources dependencies.
    /// </summary>
    /// <param name="services">IServiceCollection for the dependencies to be injected at.</param>
    /// <returns>IServiceCollection with dependencies injected.</returns>
    public static IServiceCollection AddLocalResources(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPasswordHashingService, PasswordHashingService>();
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

        return services;
    }

    /// <summary>
    /// Add Presentation's layers dependencies.
    /// </summary>
    /// <param name="services">IServiceCollection for the dependencies to be injected at.</param>
    /// <returns>IServiceCollection with dependencies injected.</returns>
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();

        services.AddMappings();

        services.AddControllers()
            .ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

        services.AddRazorPages();

        services.AddMvc();

        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(options =>
         {
             options.SwaggerDoc("v1", new OpenApiInfo
             {
                 Version = "v1",
                 Title = "TekChallenge - Authentication Service Api",
                 Description = "The Authentication Service Api is part of the TekChallenge Distributed Application backend, and it provides functionalities for Managing Users, Role based Authorization and Basic Authentication with Json Web Tokens.",
             });

             var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
             options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

             options.AddSecurityDefinition("Token", new OpenApiSecurityScheme
             {
                 Type = SecuritySchemeType.Http,
                 In = ParameterLocation.Header,
                 Name = HeaderNames.Authorization,
                 Scheme = "Bearer",
             });
             options.OperationFilter<SecureEndpointAuthRequirementFilter>();
         });

        services.AddCors(options =>
        {
            options.AddDefaultPolicy(
                policy =>
                {
                    policy.AllowAnyOrigin();
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                });
        });

        services.Configure<FormOptions>(o =>
        {
            o.ValueLengthLimit = 100000000;
            o.MultipartBodyLengthLimit = 100000000;
            o.MemoryBufferThreshold = 100000000;
        });
        return services;
    }

    /// <summary>
    /// Add dependencies for mapster type mapping.
    /// </summary>
    /// <param name="services">Injected services.</param>
    /// <returns>IServiceCollection with dependencies injected.</returns>
    public static IServiceCollection AddMappings(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());

        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();

        return services;
    }
}
