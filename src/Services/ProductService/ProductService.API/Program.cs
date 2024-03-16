// <copyright file="Program.cs" company="Tek">
// Copyright (c) TekChallenge.Services.ProductService. All rights reserved.
// </copyright>

using Microsoft.AspNetCore.Diagnostics;
using TekChallenge.Services.ProductService.API;
using TekChallenge.Services.ProductService.Application;
using TekChallenge.Services.ProductService.Infrastructure;
using TekChallenge.SharedDefinitions.Application;
using TekChallenge.SharedDefinitions.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder
        .AddSharedDefinitionsInfrastructure("ProductService")
        .AddProductServicePersistance(builder.Configuration)
        .AddSharedDefinitionsApplication(typeof(TekChallenge.Services.ProductService.Application.AssemblyAnchor).Assembly)
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

    app.Run();
}