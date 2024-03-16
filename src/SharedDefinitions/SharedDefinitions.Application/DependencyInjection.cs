// <copyright file="DependencyInjection.cs" company="Tek">
// Copyright (c) TekChallenge.Services.ProductService. All rights reserved.
// </copyright>

using TekChallenge.SharedDefinitions.Application;
using TekChallenge.SharedDefinitions.Application.Common.Behaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TekChallenge.SharedDefinitions.Domain.Common.Abstractions;

namespace TekChallenge.SharedDefinitions.Application;

/// <summary>
/// Provides support for Dependency Injection.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Injects all dependency for the Application Layer.
    /// </summary>
    /// <param name="services">IServiceCollection instance.</param>
    /// <param name="assembly">The current running assembly (dll) to register the mediator types from.</param>
    /// <returns>IServiceCollection with dependencies injected.</returns>
    public static IServiceCollection AddSharedDefinitionsApplication(this IServiceCollection services, Assembly assembly)
    {
        services.AddMediatR(assembly);

        services.AddValidatorsFromAssembly(assembly, includeInternalTypes: true);

        return services;
    }

    /// <summary>
    /// Injects all dependency for the MediatR.
    /// </summary>
    /// <param name="services">IServiceCollection instance.</param>
    /// <param name="assembly">Assembly.</param>
    /// <returns>IServiceCollection with dependencies injected.</returns>
    private static IServiceCollection AddMediatR(this IServiceCollection services, Assembly assembly)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(assembly);
        });

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(PipelineRequestValidationBehavior<,>));

        return services;
    }
}
