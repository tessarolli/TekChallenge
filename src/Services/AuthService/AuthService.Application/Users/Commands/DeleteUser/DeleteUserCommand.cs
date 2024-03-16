// <copyright file="DeleteUserCommand.cs" company="Tek">
// Copyright (c) TekChallenge.Services.AuthService. All rights reserved.
// </copyright>

using TekChallenge.SharedDefinitions.Application.Abstractions.Messaging;

namespace TekChallenge.Services.AuthService.Application.Users.Commands.DeleteUser;

/// <summary>
/// Command to Delete a User from the repository.
/// </summary>
/// <param name="Id">The Id of the User being deleted.</param>
public record DeleteUserCommand(long Id) : ICommand;
