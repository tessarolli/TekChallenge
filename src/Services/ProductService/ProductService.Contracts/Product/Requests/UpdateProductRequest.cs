// <copyright file="UpdateProductRequest.cs" company="Tek">
// Copyright (c) TekChallenge.Services.ProductService. All rights reserved.
// </copyright>

namespace TekChallenge.Services.ProductService.Contracts.Product.Requests;

/// <summary>
/// A request to update the Product in the repository.
/// </summary>
/// <param name="Id">The Product's Id to update.</param>
/// <param name="OwnerId">The updated Product's Owner Id.</param>
/// <param name="Name">The updated Product's Name.</param>
/// <param name="Description">The updated Product's Description.</param>
/// <param name="BasePrice">The updated Product's Base BasePrice.</param>
/// <param name="Stock">The updated Product's Quantity in Stock.</param>
public record struct UpdateProductRequest(
    long Id,
    long OwnerId,
    string Name,
    string Description,
    decimal BasePrice,
    int Stock);
