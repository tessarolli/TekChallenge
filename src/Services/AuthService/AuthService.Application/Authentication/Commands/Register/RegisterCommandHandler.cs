﻿// <copyright file="RegisterCommandHandler.cs" company="Tek">
// Copyright (c) TekChallenge.Services.AuthService. All rights reserved.
// </copyright>

using FluentResults;
using TekChallenge.Services.AuthService.Application.Abstractions.Authentication;
using TekChallenge.Services.AuthService.Application.Abstractions.Repositories;
using TekChallenge.Services.AuthService.Application.Authentication.Errors;
using TekChallenge.Services.AuthService.Application.Authentication.Results;
using TekChallenge.Services.AuthService.Domain.Users;
using TekChallenge.SharedDefinitions.Application.Abstractions.Messaging;
using TekChallenge.SharedDefinitions.Domain.Common.Abstractions;
using TekChallenge.SharedDefinitions.Application.Common.Errors;

namespace TekChallenge.Services.AuthService.Application.Authentication.Commands.Register;

/// <summary>
/// The implementation for the Register Command.
/// </summary>
public class RegisterCommandHandler : ICommandHandler<RegisterCommand, AuthenticationResult>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHashingService _passwordHasher;

    /// <summary>
    /// Initializes a new instance of the <see cref="RegisterCommandHandler"/> class.
    /// </summary>
    /// <param name="userRepository">IUserRepository being injected.</param>
    /// <param name="jwtTokenGenerator">IJwtTokenGenerator being injected.</param>
    /// <param name="passwordHasher">IPasswordHashingService being injected.</param>
    public RegisterCommandHandler(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator, IPasswordHashingService passwordHasher)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    /// <summary>
    /// The actual command Handler implementation for registering a new user.
    /// </summary>
    /// <param name="command">RegisterCommand.</param>
    /// <param name="cancellationToken">Async CancellationToken.</param>
    /// <returns>FluentResult for the operation.</returns>
    public async Task<Result<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        // Check if a user with the given e-mail already exists
        // Although this is a business rule, it's more of an application complexity than a domain complexity
        // Therefore, we can handle this rule enforcement here in the application layer.
        var getUserByEmailResult = await _userRepository.GetByEmailAsync(command.Email);
        if (getUserByEmailResult.IsFailed)
        {
            if (!getUserByEmailResult.HasError<NotFoundError>())
            {
                return Result.Fail(getUserByEmailResult.Errors);
            }
        }
        else
        {
            return Result.Fail(new UserWithEmailAlreadyExistsError());
        }

        // Now that we Ensure the Business Rule, we can carry on with the User Creation
        // and persisting it to the repository.
        var userResult = User.Create(null, command.FirstName, command.LastName, command.Email, command.Password, passwordHasher: _passwordHasher);
        if (userResult.IsFailed)
        {
            return Result.Fail(userResult.Errors);
        }

        // Persist newly created User Entity Instance to the Repository
        var persistResult = await _userRepository.AddAsync(userResult.Value);
        if (persistResult.IsFailed)
        {
            return Result.Fail(persistResult.Errors);
        }

        // Generate Token
        var token = _jwtTokenGenerator.GenerateToken(persistResult.Value);

        return new AuthenticationResult(persistResult.Value, token);
    }
}