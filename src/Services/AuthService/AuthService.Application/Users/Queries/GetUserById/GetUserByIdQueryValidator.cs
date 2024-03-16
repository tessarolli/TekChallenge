// <copyright file="GetUserByIdQueryValidator.cs" company="Tek">
// Copyright (c) TekChallenge.Services.AuthService. All rights reserved.
// </copyright>

using FluentValidation;

namespace TekChallenge.Services.AuthService.Application.Users.Queries.GetUserById;

/// <summary>
/// Validator for the <see cref="GetUserByIdQuery"/>.
/// </summary>
public class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetUserByIdQueryValidator"/> class.
    /// </summary>
    public GetUserByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .GreaterThan(0)
                .WithMessage("User Id cannot be empty");
    }
}
