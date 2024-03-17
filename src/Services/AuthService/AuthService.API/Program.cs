// <copyright file="Program.cs" company="Tek">
// Copyright (c) AuthService. All rights reserved.
// </copyright>

using Google.Api;
using Microsoft.AspNetCore.Diagnostics;
using Serilog;
using System.Reflection;
using TekChallenge.Services.AuthService.API;
using TekChallenge.SharedDefinitions.Application;
using TekChallenge.SharedDefinitions.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder
        .AddSharedDefinitionsInfrastructure("AuthService")
        .AddLocalResources()
        .AddSharedDefinitionsApplication(typeof(TekChallenge.Services.AuthService.Application.AssemblyAnchor).Assembly)
        .AddPresentation();
}

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    else
    {
        app.UseDefaultFiles();
        app.UseStaticFiles();
    }

    app.UseExceptionHandler("/error");

    app.Map("/error", (HttpContext context) =>
    {
        Exception? exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;

        return Results.Problem(exception?.Message);
    });

    app.UseHttpsRedirection();

    app.UseCors();

    app.MapControllers();

    app.UseAuthorization();

    app.UseSerilogRequestLogging();

    app.Run();
}