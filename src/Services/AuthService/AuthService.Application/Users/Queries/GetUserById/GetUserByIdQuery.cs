// <copyright file="GetUserByIdQuery.cs" company="Tek">
// Copyright (c) TekChallenge.Services.AuthService. All rights reserved.
// </copyright>

using TekChallenge.Services.AuthService.Application.Users.Dtos;
using TekChallenge.SharedDefinitions.Application.Abstractions.Messaging;

namespace TekChallenge.Services.AuthService.Application.Users.Queries.GetUserById;

/// <summary>
/// Gets the User by its Id.
/// </summary>
public record GetUserByIdQuery(long Id) : IQuery<UserDto>;
