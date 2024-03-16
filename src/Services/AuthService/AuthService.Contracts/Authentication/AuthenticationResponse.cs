// <copyright file="AuthenticationResponse.cs" company="Tek">
// Copyright (c) AuthService. All rights reserved.
// </copyright>

namespace TekChallenge.Services.AuthService.Contracts.Authentication;

/// <summary>
/// Contract for an Authentication Request's Response
/// </summary>
/// <param name="Id">The Authenticated User's Id.</param>
/// <param name="FirstName">The Authenticated User's First Name.</param>
/// <param name="LastName">The Authenticated User's Last Name.</param>
/// <param name="Email">The Authenticated User's Email.</param>
/// <param name="Role">The Authenticated User's Role.</param>
/// <param name="Token">The Authenticated User's Token.</param>
public record AuthenticationResponse(
    long Id,
    string FirstName,
    string LastName,
    string Email,
    int Role,
    string Token);
