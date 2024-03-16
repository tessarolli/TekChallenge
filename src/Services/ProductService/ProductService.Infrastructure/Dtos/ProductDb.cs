// <copyright file="ProductDto.cs" company="Tek">
// Copyright (c) TekChallenge.Services.ProductService. All rights reserved.
// </copyright>

namespace TekChallenge.Services.ProductService.Infrastructure.DataTransferObjects;

/// <summary>
/// Product model as a representation of the database schema.
/// </summary>
public record ProductDb(
    int id,
    long owner_id,
    string name,
    string description,
    int status_name,
    int stock,
    decimal price,
    DateTime created_at_utc);