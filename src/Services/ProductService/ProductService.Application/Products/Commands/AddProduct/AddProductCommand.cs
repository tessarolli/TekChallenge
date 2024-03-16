// <copyright file="AddProductCommand.cs" company="Tek">
// Copyright (c) TekChallenge.Services.ProductService. All rights reserved.
// </copyright>

using TekChallenge.Services.ProductService.Application.Products.Dtos;
using TekChallenge.SharedDefinitions.Application.Abstractions.Messaging;

namespace TekChallenge.Services.ProductService.Application.Products.Commands.AddProduct;

/// <summary>
/// Command to Add a Product to the catalog.
/// </summary>
/// <param name="OwnerId">The Product's Owner Id.</param>
/// <param name="Name">The Product's Name.</param>
/// <param name="Description">(Optional) The Product's Description.</param>
/// <param name="BasePrice">The Product's Base Price.</param>
public record AddProductCommand(
    long OwnerId,
    string Name,
    string Description,
    decimal BasePrice) : ICommand<ProductDto>;
