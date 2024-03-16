// <copyright file="GetUsersListQueryValidator.cs" company="Tek">
// Copyright (c) TekChallenge.Services.AuthService. All rights reserved.
// </copyright>

using FluentValidation;

namespace TekChallenge.Services.AuthService.Application.Users.Queries.GetUsersList;

/// <summary>
/// Validator.
/// </summary>
public class GetUsersListQueryValidator : AbstractValidator<GetUsersListQuery>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetUsersListQueryValidator"/> class.
    /// </summary>
    public GetUsersListQueryValidator()
    {
    }
}
