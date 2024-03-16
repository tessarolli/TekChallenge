// <copyright file="ProductResponse.cs" company="Tek">
// Copyright (c) TekChallenge.Services.ProductService. All rights reserved.
// </copyright>

namespace TekChallenge.Services.ProductService.Contracts.Product.Responses;

/// <summary>
/// The Contract for Product Response.
/// Contract for a Product Aggregate Instance Response.
/// Can be used in lists.
/// </summary>
/// <param name="Id">The Product's Id.</param>
/// <param name="OwnerId">The Product's Owner Id.</param>
/// <param name="Name">The Product's Name.</param>
/// <param name="Description">The Product's Description.</param>
/// <param name="StatusName">The Product's Status Name.</param>
/// <param name="Price">The Product's Base Price.</param>
/// <param name="Stock">The Product's Quantity in Stock.</param>
/// <param name="Discount">The Product's Discount Percentage.</param>
/// <param name="FinalPrice">The Product's Final Price with Discounts Applied.</param>
/// <param name="CreatedAtUtc">The Product's Creation Date and Time in UTC.</param>
public record ProductResponse(
    long Id,
    string OwnerId,
    string Name,
    string Description,
    int StatusName,
    int Stock,
    decimal Price,
    decimal Discount,
    decimal FinalPrice,
    DateTime CreatedAtUtc);
