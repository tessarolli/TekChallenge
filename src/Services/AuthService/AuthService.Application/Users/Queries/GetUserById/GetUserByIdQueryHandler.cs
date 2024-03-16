// <copyright file="GetUserByIdQueryHandler.cs" company="Tek">
// Copyright (c) TekChallenge.Services.AuthService. All rights reserved.
// </copyright>

using FluentResults;
using TekChallenge.Services.AuthService.Application.Abstractions.Repositories;
using TekChallenge.Services.AuthService.Application.Users.Dtos;
using TekChallenge.Services.AuthService.Domain.Users.ValueObjects;
using TekChallenge.SharedDefinitions.Application.Abstractions.Messaging;

namespace TekChallenge.Services.AuthService.Application.Users.Queries.GetUserById;

/// <summary>
/// Mediator Handler for the <see cref="GetUserByIdQuery"/>.
/// </summary>
public class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, UserDto>
{
    private readonly IUserRepository _userRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetUserByIdQueryHandler"/> class.
    /// </summary>
    /// <param name="userRepository">Injected UserRepository.</param>
    public GetUserByIdQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    /// <inheritdoc/>
    public async Task<Result<UserDto>> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
    {
        var userDomainModel = await _userRepository.GetByIdAsync(query.Id);
        if (!userDomainModel.IsSuccess)
        {
            return Result.Fail(userDomainModel.Errors);
        }

        return Result.Ok(new UserDto(
            userDomainModel.Value.Id.Value,
            userDomainModel.Value.FirstName,
            userDomainModel.Value.LastName,
            userDomainModel.Value.Email,
            userDomainModel.Value.Password.Value,
            (int)userDomainModel.Value.Role,
            userDomainModel.Value.CreatedAtUtc));
    }
}