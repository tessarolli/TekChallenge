// <copyright file="GetProductByIdQueryValidator.cs" company="Tek">
// Copyright (c) TekChallenge.Services.ProductService. All rights reserved.
// </copyright>

using FluentValidation;

namespace TekChallenge.Services.ProductService.Application.Products.Queries.GetProductById;

/// <summary>
/// Validator for the <see cref="GetProductByIdQuery"/>.
/// </summary>
public class GetProductByIdQueryValidator : AbstractValidator<GetProductByIdQuery>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetProductByIdQueryValidator"/> class.
    /// </summary>
    public GetProductByIdQueryValidator()
    {
        RuleFor(x => x.Id)
          .NotEmpty()
              .WithMessage("Product Id cannot be empty")
          .GreaterThan(0)
              .WithMessage("Product Id have to be greater than zero");
    }
}
