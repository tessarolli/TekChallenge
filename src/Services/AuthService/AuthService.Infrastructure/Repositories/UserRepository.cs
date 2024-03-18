// <copyright file="UserRepository.cs" company="Tek">
// Copyright (c) AuthService. All rights reserved.
// </copyright>

using System.Data;
using System.Data.Common;
using FluentResults;
using Microsoft.Extensions.Logging;
using TekChallenge.Services.AuthService.Application.Abstractions.Repositories;
using TekChallenge.Services.AuthService.Contracts.Enums;
using TekChallenge.Services.AuthService.Domain.Users;
using TekChallenge.Services.AuthService.Domain.Users.ValueObjects;
using TekChallenge.Services.AuthService.Infrastructure.Dtos;
using TekChallenge.SharedDefinitions.Application.Common.Errors;
using TekChallenge.SharedDefinitions.Domain.Common.Abstractions;
using TekChallenge.SharedDefinitions.Infrastructure.Abstractions;

namespace TekChallenge.Services.AuthService.Infrastructure.Repositories;

/// <summary>
/// The User Repository.
/// </summary>
public class UserRepository : IUserRepository
{
    private readonly ILogger _logger;
    private readonly IPasswordHashingService _passwordHasher;
    private readonly IDapperUtility _db;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserRepository"/> class.
    /// </summary>
    /// <param name="dapperUtility">IDapperUtility to inject.</param>
    /// <param name="connectionFactory">ISqlConnectionFactory to inject.</param>
    /// <param name="passwordHasher">IPasswordHashingService to inject.</param>
    /// <param name="logger">ILogger to inject.</param>
    public UserRepository(IDapperUtility dapperUtility, ISqlConnectionFactory<DbConnection> connectionFactory, ILogger<UserRepository> logger, IPasswordHashingService passwordHasher)
    {
        _logger = logger;
        _passwordHasher = passwordHasher;
        _db = dapperUtility;
    }

    /// <inheritdoc/>
    public async Task<Result<User>> GetByIdAsync(long id)
    {
        var sql = "SELECT * FROM users WHERE id = @id";

        var UserDB = await _db.QueryFirstOrDefaultAsync<UserDb>(sql, new { id });

        return CreateUserResultFromUserDB(UserDB);
    }

    /// <inheritdoc/>
    public async Task<List<User>> GetByIdsAsync(long[] ids)
    {
        _logger.LogInformation("UserRepository.GetByIdsAsync({email})", string.Join(',', ids));

        var sql = "SELECT * FROM users WHERE id = ANY(@ids)";

        var parameters = new
        {
            ids,
        };

        var UserDBs = await _db.QueryAsync<UserDb>(sql, new { ids });

        return UserDBs
            .Select(CreateUserResultFromUserDB)
            .Where(x => x.IsSuccess)
            .Select(x => x.Value)
            .ToList();
    }

    /// <inheritdoc/>
    public async Task<Result<List<User>>> GetAllAsync()
    {
        var sql = "SELECT * FROM users";

        var UserDBs = await _db.QueryAsync<UserDb>(sql, null);

        return UserDBs
            .Select(CreateUserResultFromUserDB)
            .Where(x => x.IsSuccess)
            .Select(x => x.Value)
            .ToList();
    }

    /// <inheritdoc/>
    public async Task<Result<User>> GetByEmailAsync(string email)
    {
        _logger.LogInformation("UserRepository.GetByEmailAsync({email})", email);

        var sql = "SELECT * FROM users WHERE email = @email";

        var UserDB = await _db.QueryFirstOrDefaultAsync<UserDb>(sql, new { email });

        return CreateUserResultFromUserDB(UserDB);
    }

    /// <inheritdoc/>
    public async Task<Result<User>> AddAsync(User user)
    {
        if (!user.Password.IsHashed)
        {
            user.Password.HashPassword(_passwordHasher);
        }

        var sql = "INSERT INTO users (" +
                      "first_name," +
                      "last_name," +
                      "email," +
                      "password," +
                      "role) " +
                  "VALUES (" +
                      "@FirstName," +
                      "@LastName," +
                      "@Email," +
                      "@Password," +
                      "@Role) " +
                  "RETURNING " +
                      "id";

        var parameters = new
        {
            user.FirstName,
            user.LastName,
            user.Email,
            Password = user.Password.HashedPassword,
            Role = (int)user.Role,
        };

        var newId = await _db.ExecuteScalarAsync(sql, parameters);

        return await GetByIdAsync(newId);
    }

    /// <inheritdoc/>
    public async Task<Result<User>> UpdateAsync(User user)
    {
        if (!user.Password.IsHashed)
        {
            user.Password.HashPassword(_passwordHasher);
        }

        var parameters = new
        {
            p_id = user.Id.Value,
            p_first_name = user.FirstName,
            p_last_name = user.LastName,
            p_email = user.Email,
            p_password = user.Password.HashedPassword,
            p_role = user.Role,
        };

        await _db.ExecuteAsync("sp_update_user", parameters, CommandType.StoredProcedure);

        return Result.Ok(user);
    }

    /// <inheritdoc/>
    public async Task<Result> RemoveAsync(UserId userId)
    {
        var parameters = new
        {
            removing_user_id = userId.Value,
        };

        await _db.ExecuteAsync("remove_user", parameters, CommandType.StoredProcedure);

        return Result.Ok();
    }

    /// <summary>
    /// Creates a new instance of the User Domain Model Entity, using the Create factory method.
    /// </summary>
    /// <param name="user">The User Data Transfer Object.</param>
    /// <returns>An Result indicating the status of the operation.</returns>
    private static Result<User> CreateUserResultFromUserDB(UserDb? user)
    {
        if (user is null)
        {
            return Result.Fail(new NotFoundError($"User not found."));
        }

        var userDomainModel = User.Create(
                user.id,
                user.first_name,
                user.last_name,
                user.email,
                user.password,
                (Roles)user.role);

        if (!userDomainModel.IsSuccess)
        {
            return Result.Fail(userDomainModel.Errors);
        }

        return Result.Ok(userDomainModel.Value);
    }
}
