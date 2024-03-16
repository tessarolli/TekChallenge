// <copyright file="DeleteProductCommandValidator.cs" company="Tek">
// Copyright (c) TekChallenge.Services.ProductService. All rights reserved.
// </copyright>

using FluentValidation;

namespace TekChallenge.Services.ProductService.Application.Products.Commands.DeleteProduct;

/// <summary>
/// Validator for the <see cref="DeleteProductCommand"/>.
/// </summary>
public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteProductCommandValidator"/> class.
    /// </summary>
    public DeleteProductCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
                .WithMessage("Product Id cannot be empty")
            .GreaterThan(0)
                .WithMessage("Product Id have to be greater than zero");
    }
}
