// <copyright file="ProductDto.cs" company="Tek">
// Copyright (c) TekChallenge.Services.ProductService. All rights reserved.
// </copyright>

using TekChallenge.Services.ProductService.Domain.Enums;

namespace TekChallenge.Services.ProductService.Application.Products.Dtos;

/// <summary>
/// Contract for the Product Data Transfer Object.
/// </summary>
public record ProductDto(
    long Id,
    long OwnerId,
    string Name,
    string Description,
    StatusName StatusName,
    int Stock,
    decimal Price,
    decimal Discount,
    decimal FinalPrice,
    DateTime CreatedAtUtc);