// <copyright file="RegisterRequest.cs" company="Tek">
// Copyright (c) AuthService. All rights reserved.
// </copyright>

namespace TekChallenge.Services.AuthService.Contracts.Authentication;

/// <summary>
/// Contract for the Register Request.
/// </summary>
/// <param name="FirstName">The User's First Name.</param>
/// <param name="LastName">The User's Last Name</param>
/// <param name="Email">The User's E-mail</param>
/// <param name="Password">The User's Password</param>
public record struct RegisterRequest(
    string FirstName,
    string LastName,
    string Email,
    string Password);
