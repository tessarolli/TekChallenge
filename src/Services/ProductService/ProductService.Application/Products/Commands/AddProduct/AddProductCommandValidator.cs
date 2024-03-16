// <copyright file="AddProductCommandValidator.cs" company="Tek">
// Copyright (c) TekChallenge.Services.ProductService. All rights reserved.
// </copyright>

using FluentValidation;
using TekChallenge.Services.ProductService.Application.Products.Commands.AddProduct;

namespace TekChallenge.Services.ProductService.Application.Products.Commands.AddProduct;

/// <summary>
/// Validator for the <see cref="AddProductCommand"/>.
/// </summary>
public class AddProductCommandValidator : AbstractValidator<AddProductCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AddProductCommandValidator"/> class.
    /// </summary>
    public AddProductCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(255);

        RuleFor(x => x.BasePrice)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.OwnerId)
            .NotEmpty()
            .GreaterThan(0);
    }
}
