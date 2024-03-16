// <copyright file="IProductRepository.cs" company="Tek">
// Copyright (c) TekChallenge.Services.ProductService. All rights reserved.
// </copyright>

using System.Data.Common;
using TekChallenge.Services.ProductService.Domain.Products;
using TekChallenge.Services.ProductService.Domain.Products.ValueObjects;
using FluentResults;

namespace TekChallenge.Services.ProductService.Application.Abstractions.Repositories;

/// <summary>
/// The Product Repository Interface.
/// </summary>
public interface IProductRepository
{
    /// <summary>
    /// Get an Product aggregate by Id.
    /// </summary>
    /// <param name="id">The Product Id.</param>
    /// <param name="transaction">The Transaction.</param>
    /// <returns>A Result with the Product Aggregate, or a error message.</returns>
    Task<Result<Product>> GetByIdAsync(ProductId id, DbTransaction? transaction = null);

    /// <summary>
    /// Gets a List of all Products from the repository.
    /// </summary>
    /// <returns>A Result indicating the status of this operation.</returns>
    Task<Result<List<Product>>> GetAllAsync();

    /// <summary>
    /// Add an Product into the Repository.
    /// </summary>
    /// <param name="Product">The Product to Add.</param>
    /// <returns>A Result indicating the status of this operation.</returns>
    Task<Result<Product>> AddAsync(Product Product);

    /// <summary>
    /// Update the Product in the Repository.
    /// </summary>
    /// <param name="Product">The Product to Update.</param>
    /// <returns>A Result indicating the status of this operation.</returns>
    Task<Result<Product>> UpdateAsync(Product Product);

    /// <summary>
    /// Remove the Product from the Repository.
    /// </summary>
    /// <param name="ProductId">The Product to Remove.</param>
    /// <returns>A Result indicating the status of this operation.</returns>
    Task<Result> RemoveAsync(ProductId ProductId);
}
