// <copyright file="IJwtTokenGenerator.cs" company="Tek">
// Copyright (c) TekChallenge.Services.AuthService. All rights reserved.
// </copyright>

using TekChallenge.Services.AuthService.Domain.Users;

namespace TekChallenge.Services.AuthService.Application.Abstractions.Authentication;

/// <summary>
/// Interface for Json Web Tokens Generator.
/// </summary>
public interface IJwtTokenGenerator
{
    /// <summary>
    /// Generates a JWT token.
    /// </summary>
    /// <param name="user">The User to generate the token for.</param>
    /// <returns>The token.</returns>
    string GenerateToken(User user);
}
