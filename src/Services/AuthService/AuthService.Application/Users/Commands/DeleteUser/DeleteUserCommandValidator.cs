// <copyright file="DeleteUserCommandValidator.cs" company="Tek">
// Copyright (c) TekChallenge.Services.AuthService. All rights reserved.
// </copyright>

using FluentValidation;

namespace TekChallenge.Services.AuthService.Application.Users.Commands.DeleteUser;

/// <summary>
/// Validator for the <see cref="DeleteUserCommand"/>.
/// </summary>
public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteUserCommandValidator"/> class.
    /// </summary>
    public DeleteUserCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .GreaterThan(0)
                .WithMessage("User Id cannot be empty");
    }
}
