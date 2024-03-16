// <copyright file="GetProductsListQueryValidator.cs" company="Tek">
// Copyright (c) TekChallenge.Services.ProductService. All rights reserved.
// </copyright>

using FluentValidation;

namespace TekChallenge.Services.ProductService.Application.Products.Queries.GetProductsList;

/// <summary>
/// Validator for the <see cref="GetProductsListQuery"/>.
/// </summary>
public class GetProductsListQueryValidator : AbstractValidator<GetProductsListQuery>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetProductsListQueryValidator"/> class.
    /// </summary>
    public GetProductsListQueryValidator()
    {
    }
}
