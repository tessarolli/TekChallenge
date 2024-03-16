// <copyright file="UpdateUserRequest.cs" company="Tek">
// Copyright (c) AuthService. All rights reserved.
// </copyright>

namespace TekChallenge.Services.AuthService.Contracts.User.Requests;

/// <summary>
/// A request to update the User in the repository.
/// </summary>
/// <param name="Id">The User's Id.</param>
/// <param name="FirstName">The User's First name.</param>
/// <param name="LastName">The User's Last name.</param>
/// <param name="Email">The User's E-Mail.</param>
/// <param name="Password">The User's Password.</param>
/// <param name="Role">The User's Role. 0 = User, 1 = Admin</param>
public record struct UpdateUserRequest(
    long Id,
    string FirstName,
    string LastName,
    string Email,
    string Password,
    int Role);