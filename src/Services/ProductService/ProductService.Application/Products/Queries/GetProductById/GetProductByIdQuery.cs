// <copyright file="GetProductByIdQuery.cs" company="Tek">
// Copyright (c) TekChallenge.Services.ProductService. All rights reserved.
// </copyright>

using TekChallenge.Services.ProductService.Application.Products.Dtos;
using TekChallenge.SharedDefinitions.Application.Abstractions.Messaging;

namespace TekChallenge.Services.ProductService.Application.Products.Queries.GetProductById;

/// <summary>
/// Gets the Product by its Id.
/// </summary>
/// <param name="Id">The Id of the product being deleted.</param>
public record GetProductByIdQuery(long Id) : IQuery<ProductDto>;
