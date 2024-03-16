// <copyright file="AuthenticatedUserService.cs" company="Tek">
// Copyright (c) AuthService. All rights reserved.
// </copyright>

using Microsoft.AspNetCore.Http;
using TekChallenge.SharedDefinitions.Application.Abstractions.Services;
using TekChallenge.SharedDefinitions.Application.Dtos;
using System.Security.Authentication;
using TekChallenge.Services.AuthService.Application.Abstractions.Repositories;

namespace TekChallenge.Services.AuthService.Infrastructure.Services;

/// <summary>
/// Get the Authenticated User Entity from the Json Web Token.
/// </summary>
public class AuthenticatedUserService : IAuthenticatedUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthenticatedUserService"/> class.
    /// </summary>
    /// <param name="userRepository">Inject User Repository.</param>
    /// <param name="httpContextAccessor">Inject HttpContextAccessor.</param>
    public AuthenticatedUserService(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
    {
        _userRepository = userRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    /// <inheritdoc/>
    public async Task<AuthenticatedUserDto?> GetAuthenticatedUserAsync()
    {
        var userId = _httpContextAccessor?.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == "jti")?.Value;

        if (userId is null)
        {
            throw new InvalidCredentialException("User information not present in the request.");
        }

        var userResult = await _userRepository.GetByIdAsync(long.Parse(userId));
        if (!userResult.IsSuccess)
        {
            throw new UnauthorizedAccessException("User not authenticated.");
        }

        return new AuthenticatedUserDto(
                userResult.Value.Id.Value,
                userResult.Value.FirstName,
                userResult.Value.LastName,
                userResult.Value.Email,
                (int)userResult.Value.Role);
    }
}