// <copyright file="UpdateProductCommand.cs" company="Tek">
// Copyright (c) TekChallenge.Services.ProductService. All rights reserved.
// </copyright>

using TekChallenge.Services.ProductService.Application.Products.Dtos;
using TekChallenge.SharedDefinitions.Application.Abstractions.Messaging;

namespace TekChallenge.Services.ProductService.Application.Products.Commands.UpdateProduct;

/// <summary>
/// Command to Update a Product in the catalog.
/// </summary>
/// <param name="Id">The Product's Id to update.</param>
/// <param name="OwnerId">The updated Product's Owner Id.</param>
/// <param name="Name">The updated Product's Name.</param>
/// <param name="Description">The updated Product's Description.</param>
/// <param name="BasePrice">The updated Product's Base Price.</param>
/// <param name="Stock">The updated Product's Quantity in Stock.</param>
public record UpdateProductCommand(
    long Id,
    long OwnerId,
    string Name,
    string Description,
    decimal BasePrice,
    int Stock) : ICommand<ProductDto>;
