// <copyright file="AddUserCommandValidator.cs" company="Tek">
// Copyright (c) TekChallenge.Services.AuthService. All rights reserved.
// </copyright>

using FluentValidation;

namespace TekChallenge.Services.AuthService.Application.Users.Commands.AddUser;

/// <summary>
/// Validator for the <see cref="AddUserCommand"/>.
/// </summary>
public class AddUserCommandValidator : AbstractValidator<AddUserCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AddUserCommandValidator"/> class.
    /// </summary>
    public AddUserCommandValidator()
    {
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
