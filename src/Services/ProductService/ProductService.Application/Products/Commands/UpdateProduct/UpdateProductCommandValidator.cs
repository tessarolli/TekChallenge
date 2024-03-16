// <copyright file="UpdateProductCommandValidator.cs" company="Tek">
// Copyright (c) TekChallenge.Services.ProductService. All rights reserved.
// </copyright>

using FluentValidation;

namespace TekChallenge.Services.ProductService.Application.Products.Commands.UpdateProduct;

/// <summary>
/// Validator for the <see cref="UpdateProductCommand"/>.
/// </summary>
public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateProductCommandValidator"/> class.
    /// </summary>
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Id)
           .NotEmpty()
           .GreaterThan(0);

        RuleFor(x => x.OwnerId)
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(255);

        RuleFor(x => x.BasePrice)
            .GreaterThanOrEqualTo(0);
    }
}
