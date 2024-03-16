// <copyright file="UpdateUserCommandValidator.cs" company="Tek">
// Copyright (c) TekChallenge.Services.AuthService. All rights reserved.
// </copyright>

using FluentValidation;

namespace TekChallenge.Services.AuthService.Application.Users.Commands.UpdateUser;

/// <summary>
/// Validator for the <see cref="UpdateUserCommand"/>.
/// </summary>
public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateUserCommandValidator"/> class.
    /// </summary>
    public UpdateUserCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotNull()
            .GreaterThan(0)
                .WithMessage("User Id is required.");

        RuleFor(x => x.FirstName)
            .NotEmpty()
              .WithMessage("First Name cannot be empty")
            .MaximumLength(50)
              .WithMessage("First Name cannot be larger than 50 characteres");

        RuleFor(x => x.LastName)
            .NotEmpty()
                .WithMessage("First Name cannot be empty")
            .MaximumLength(50)
                .WithMessage("First Name cannot be larger than 50 characteres");

        RuleFor(x => x.Email)
            .EmailAddress()
                .WithMessage("Email is Invalid");

        RuleFor(x => x.Password)
            .NotEmpty()
                .WithMessage("Password is required");
    }
}
