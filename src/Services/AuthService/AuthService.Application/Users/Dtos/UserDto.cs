// <copyright file="UserResult.cs" company="Tek">
// Copyright (c) TekChallenge.Services.AuthService. All rights reserved.
// </copyright>

using TekChallenge.Services.AuthService.Contracts.Enums;

namespace TekChallenge.Services.AuthService.Application.Users.Dtos;

/// <summary>
/// The Data Transfer Object for User.
/// </summary>
/// <param name="Id">The User's Id.</param>
/// <param name="FirstName">The User's First name.</param>
/// <param name="LastName">The User's Last name.</param>
/// <param name="Email">The User's E-Mail.</param>
/// <param name="Password">The User's Password.</param>
/// <param name="Role">The User's Role. 0 = User, 1 = Manager, 2 = Admin.</param>
/// <param name="CreatedAtUtc">The User's registration date.</param>
public record UserDto(
    long Id,
    string FirstName,
    string LastName,
    string Email,
    string Password,
    int Role,
    DateTime CreatedAtUtc);