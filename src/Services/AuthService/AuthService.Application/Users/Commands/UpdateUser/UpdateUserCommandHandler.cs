// <copyright file="UpdateUserCommandHandler.cs" company="Tek">
// Copyright (c) TekChallenge.Services.AuthService. All rights reserved.
// </copyright>

using FluentResults;
using TekChallenge.Services.AuthService.Application.Abstractions.Repositories;
using TekChallenge.Services.AuthService.Application.Users.Dtos;
using TekChallenge.Services.AuthService.Contracts.Enums;
using TekChallenge.Services.AuthService.Domain.Users;
using TekChallenge.SharedDefinitions.Application.Abstractions.Messaging;
using TekChallenge.SharedDefinitions.Domain.Common.Abstractions;

namespace TekChallenge.Services.AuthService.Application.Users.Commands.UpdateUser;

/// <summary>
/// Mediator Handler for the <see cref="UpdateUserCommand"/>.
/// </summary>
public class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand, UserDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHashingService _passwordHasher;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateUserCommandHandler"/> class.
    /// </summary>
    /// <param name="userRepository">Injected _userRepository.</param>
    /// <param name="passwordHasher">Injected _passwordHasher.</param>
    public UpdateUserCommandHandler(IUserRepository userRepository, IPasswordHashingService passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    /// <inheritdoc/>
    public async Task<Result<UserDto>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var userDomainModel = User.Create(
            request.Id,
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password,
            (Roles)request.Role,
            passwordHasher: _passwordHasher);

        if (!userDomainModel.IsSuccess)
        {
            return Result.Fail(userDomainModel.Errors);
        }

        var updateResult = await _userRepository.UpdateAsync(userDomainModel.Value);
        if (!updateResult.IsSuccess)
        {
            return Result.Fail(updateResult.Errors);
        }

        return Result.Ok(new UserDto(
            updateResult.Value.Id.Value,
            updateResult.Value.FirstName,
            updateResult.Value.LastName,
            updateResult.Value.Email,
            updateResult.Value.Password.Value,
            (int)updateResult.Value.Role,
            updateResult.Value.CreatedAtUtc));
    }
}
