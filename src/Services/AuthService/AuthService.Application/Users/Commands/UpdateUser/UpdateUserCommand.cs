// <copyright file="UpdateUserCommand.cs" company="Tek">
// Copyright (c) TekChallenge.Services.AuthService. All rights reserved.
// </copyright>

using TekChallenge.Services.AuthService.Application.Users.Dtos;
using TekChallenge.SharedDefinitions.Application.Abstractions.Messaging;

namespace TekChallenge.Services.AuthService.Application.Users.Commands.UpdateUser;

/// <summary>
/// Command to Update a User in the repository.
/// </summary>
public record UpdateUserCommand(
    long Id,
    string FirstName,
    string LastName,
    string Email,
    string Password,
    int Role) : ICommand<UserDto>;
