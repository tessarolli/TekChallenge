// <copyright file="InvalidPasswordError.cs" company="Tek">
// Copyright (c) TekChallenge.Services.AuthService. All rights reserved.
// </copyright>

using TekChallenge.SharedDefinitions.Application.Common.Errors;

namespace TekChallenge.Services.AuthService.Application.Authentication.Errors;

/// <summary>
/// Invalid Password error.
/// </summary>
public class InvalidPasswordError : UnauthorizedError
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidPasswordError"/> class.
    /// </summary>
    public InvalidPasswordError()
    {
        Message = "Invalid Password";
    }
}
