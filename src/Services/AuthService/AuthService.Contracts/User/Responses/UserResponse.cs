// <copyright file="UserResponse.cs" company="Tek">
// Copyright (c) AuthService. All rights reserved.
// </copyright>

namespace TekChallenge.Services.AuthService.Contracts.User.Responses;

/// <summary>
/// The Contract for User Response.
/// Contract for a User Aggregate Instance Response.
/// Can be used in lists.
/// </summary>
/// <param name="Id">The User's Id.</param>
/// <param name="FirstName">The User's First name.</param>
/// <param name="LastName">The User's Last name.</param>
/// <param name="Email">The User's E-Mail.</param>
/// <param name="Password">The User's Password.</param>
/// <param name="Role">The User's Role. 0 = User, 1 = Manager, 2 = Admin.</param>
/// <param name="CreatedAtUtc">The User's registration date.</param>
public record UserResponse(
    long Id,
    string FirstName,
    string LastName,
    string Email,
    string Password,
    int Role,
    DateTime CreatedAtUtc);
