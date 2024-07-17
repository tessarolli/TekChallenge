// <copyright file="SecureEndpointAuthRequirementFilter.cs" company="Tek">
// Copyright (c) AuthService. All rights reserved.
// </copyright>

using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace TekChallenge.Services.AuthService.API.Swagger;

/// <summary>
/// Requirement Filter for the Swagger UI to add Authorization.
/// </summary>
internal class SecureEndpointAuthRequirementFilter : IOperationFilter
{
    /// <summary>
    /// Apply the Requirement Filters for Swagger UI.
    /// </summary>
    /// <param name="operation">Operation.</param>
    /// <param name="context">Context.</param>
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (!context
            .ApiDescription
            .ActionDescriptor
            .EndpointMetadata
            .OfType<AuthorizeAttribute>()
            .Any())
        {
            return;
        }

        operation.Security = new List<OpenApiSecurityRequirement>
        {
            new() {
                [
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Token",
                        },
                    }
                ] = new List<string>(),
            },
        };
    }
}