// <copyright file="UserWithEmailNotFoundError.cs" company="Tek">
// Copyright (c) TekChallenge.Services.AuthService. All rights reserved.
// </copyright>

using TekChallenge.SharedDefinitions.Application.Common.Errors;

namespace TekChallenge.Services.AuthService.Application.Authentication.Errors;

/// <summary>
/// User with informed email does not exists.
/// </summary>
public class UserWithEmailNotFoundError : NotFoundError
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UserWithEmailNotFoundError"/> class.
    /// </summary>
    public UserWithEmailNotFoundError()
    {
        Message = "Account with given e-mail address does not exist";
    }
}
