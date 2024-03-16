// <copyright file="UserDb.cs" company="Tek">
// Copyright (c) TekChallenge.Services.AuthService. All rights reserved.
// </copyright>

namespace TekChallenge.Services.AuthService.Infrastructure.Dtos;

/// <summary>
/// User model as a representation of the database schema.
/// </summary>
public record UserDb(
    long id,
    string first_name,
    string last_name,
    string email,
    string password,
    int role,
    DateTime created_at_utc);