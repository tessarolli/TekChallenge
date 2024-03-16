// <copyright file="AuthenticationResult.cs" company="Tek">
// Copyright (c) TekChallenge.Services.AuthService. All rights reserved.
// </copyright>

using TekChallenge.Services.AuthService.Domain.Users;

namespace TekChallenge.Services.AuthService.Application.Authentication.Results;

/// <summary>
/// Contract for an Authentication Result response.
/// </summary>
/// <param name="User"></param>
/// <param name="Token"></param>
public record AuthenticationResult(
    User User,
    string Token);
