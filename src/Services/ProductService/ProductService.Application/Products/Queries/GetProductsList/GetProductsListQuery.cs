// <copyright file="GetProductsListQuery.cs" company="Tek">
// Copyright (c) TekChallenge.Services.ProductService. All rights reserved.
// </copyright>

using TekChallenge.SharedDefinitions.Application.Abstractions.Messaging;
using TekChallenge.Services.ProductService.Application.Products.Dtos;

namespace TekChallenge.Services.ProductService.Application.Products.Queries.GetProductsList;

/// <summary>
/// Gets the List of Products.
/// </summary>
public record GetProductsListQuery() : IQuery<List<ProductDto>>;
