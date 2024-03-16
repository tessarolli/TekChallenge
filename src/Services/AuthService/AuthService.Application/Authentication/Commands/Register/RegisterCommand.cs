// <copyright file="RegisterCommand.cs" company="Tek">
// Copyright (c) TekChallenge.Services.AuthService. All rights reserved.
// </copyright>

using TekChallenge.Services.AuthService.Application.Authentication.Results;
using FluentResults;
using MediatR;
using TekChallenge.SharedDefinitions.Application.Abstractions.Messaging;

namespace TekChallenge.Services.AuthService.Application.Authentication.Commands.Register;

/// <summary>
/// Contract for the Register Command
/// </summary>
/// <param name="FirstName"></param>
/// <param name="LastName"></param>
/// <param name="Email"></param>
/// <param name="Password"></param>
public record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password) : ICommand<AuthenticationResult>;
