using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using System.Reflection;
using TekChallenge.Services.ProductService.Domain.Products.ValueObjects;
using TekChallenge.SharedDefinitions.Application;
using TekChallenge.SharedDefinitions.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder
        .AddSharedDefinitionsInfrastructure("DiscountService")
        .AddSharedDefinitionsApplication(Assembly.GetExecutingAssembly());

    // Add services to the container.
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "TekChallenge - Discount Service Api",
            Description = "The Discount Service Api is part of the TekChallenge Distributed Application backend, and it provides functionalities for fetching the Discount Amount for the given Product Ids.",
        });
    });
}

var app = builder.Build();
{
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }


    app.MapGet("/discount/{productId:long}", (long productId) =>
    {
        var discount = new Discount(productId, new Random().Next(0, 100));
        return Results.Ok(discount);
    })
    .WithName("GetDiscount")
    .WithOpenApi();

    app.Run();
}
