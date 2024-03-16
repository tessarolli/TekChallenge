// <copyright file="AddUserRequest.cs" company="Tek">
// Copyright (c) AuthService. All rights reserved.
// </copyright>

namespace TekChallenge.Services.AuthService.Contracts.User.Requests;

/// <summary>
/// Contract for Adding a User to the repository's request.
/// </summary>
/// <param name="FirstName">The User's First name.</param>
/// <param name="LastName">The User's Last name.</param>
/// <param name="Email">The User's E-Mail.</param>
/// <param name="Password">The User's Password.</param>
public record struct AddUserRequest(
    string FirstName,
    string LastName,
    string Email,
    string Password);