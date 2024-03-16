// <copyright file="IAuthenticatedUserService.cs" company="Tek">
// Copyright (c) TekChallenge.SharedDefinitions. All rights reserved.
// </copyright>

using TekChallenge.SharedDefinitions.Application.Dtos;

namespace TekChallenge.SharedDefinitions.Application.Abstractions.Services;

/// <summary>
/// Provides funcionality for Accessing the Authenticated User Entity.
/// </summary>
public interface IAuthenticatedUserService
{
    /// <summary>
    /// Gets the Authenticated User Entity from the Json Web Token.
    /// </summary>
    /// <returns>The Authenticated User Entity instance.</returns>
    Task<AuthenticatedUserDto?> GetAuthenticatedUserAsync();
}