// <copyright file="GetUsersListQuery.cs" company="Tek">
// Copyright (c) TekChallenge.Services.AuthService. All rights reserved.
// </copyright>

using TekChallenge.Services.AuthService.Application.Users.Dtos;
using TekChallenge.SharedDefinitions.Application.Abstractions.Messaging;

namespace TekChallenge.Services.AuthService.Application.Users.Queries.GetUsersList;

/// <summary>
/// Gets the List of Users.
/// </summary>
public record GetUsersListQuery() : IQuery<List<UserDto>>;
