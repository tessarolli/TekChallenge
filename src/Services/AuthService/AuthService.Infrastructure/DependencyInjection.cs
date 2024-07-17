// <copyright file="DependencyInjection.cs" company="Tek">
// Copyright (c) TekChallenge.Services.ProductService. All rights reserved.
// </copyright>

using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TekChallenge.Services.AuthService.Application.Abstractions.Authentication;
using TekChallenge.Services.AuthService.Infrastructure.Authentication;
using TekChallenge.SharedDefinitions.Application.Abstractions.Services;

namespace TekChallenge.AuthService.Infrastructure;

/// <summary>
/// Dependency Injection.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Add Authentication dependencies.
    /// </summary>
    /// <param name="services">Injected services.</param>
    /// <returns>Services with dependencies injected.</returns>
    public static IServiceCollection AddAuthentication(this IServiceCollection services)
    {
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        return services;
    }
}
