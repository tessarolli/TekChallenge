// <copyright file="AddUserCommand.cs" company="Tek">
// Copyright (c) TekChallenge.Services.AuthService. All rights reserved.
// </copyright>

using TekChallenge.Services.AuthService.Application.Users.Dtos;
using TekChallenge.SharedDefinitions.Application.Abstractions.Messaging;

namespace TekChallenge.Services.AuthService.Application.Users.Commands.AddUser;

/// <summary>
/// Command to Add a User to the repository.
/// </summary>
public record AddUserCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password) : ICommand<UserDto>;
