// <copyright file="DeleteProductRequest.cs" company="Tek">
// Copyright (c) TekChallenge.Services.ProductService. All rights reserved.
// </copyright>

namespace TekChallenge.Services.ProductService.Contracts.Product.Requests;

/// <summary>
/// A request to delete a Product from the repository.
/// </summary>
/// <param name="Id">The Product's Id.</param>
public record DeleteProductRequest(long Id);
