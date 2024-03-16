// <copyright file="UserWithEmailAlreadyExistsError.cs" company="Tek">
// Copyright (c) TekChallenge.Services.AuthService. All rights reserved.
// </copyright>

using TekChallenge.SharedDefinitions.Application.Common.Errors;

namespace TekChallenge.Services.AuthService.Application.Authentication.Errors;

/// <summary>
/// User with this email already exists.
/// </summary>
public class UserWithEmailAlreadyExistsError : ConflictError
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UserWithEmailAlreadyExistsError"/> class.
    /// </summary>
    public UserWithEmailAlreadyExistsError()
    {
        Message = "Given E-Mail address is already in use";
    }
}
