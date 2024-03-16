// <copyright file="LoginQuery.cs" company="Tek">
// Copyright (c) TekChallenge.Services.AuthService. All rights reserved.
// </copyright>

using TekChallenge.Services.AuthService.Application.Authentication.Results;
using TekChallenge.SharedDefinitions.Application.Abstractions.Messaging;

namespace TekChallenge.Services.AuthService.Application.Authentication.Queries.Login;

/// <summary>
/// Contract for the Login Query.
/// </summary>
/// <param name="Email"></param>
/// <param name="Password"></param>
public record LoginQuery(
    string Email,
    string Password) : IQuery<AuthenticationResult>;
