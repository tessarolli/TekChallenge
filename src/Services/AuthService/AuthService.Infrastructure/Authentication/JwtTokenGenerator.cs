// <copyright file="JwtTokenGenerator.cs" company="Tek">
// Copyright (c) AuthService. All rights reserved.
// </copyright>

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TekChallenge.Services.AuthService.Application.Abstractions.Authentication;
using TekChallenge.Services.AuthService.Domain.Users;
using TekChallenge.SharedDefinitions.Infrastructure.Authentication;

namespace TekChallenge.Services.AuthService.Infrastructure.Authentication;

/// <summary>
/// Jwt Token Generator.
/// </summary>
public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly JwtSettings _jwtSettings;

    /// <summary>
    /// Initializes a new instance of the <see cref="JwtTokenGenerator"/> class.
    /// </summary>
    /// <param name="optionsJwtSettings">JwtSetting injected.</param>
    public JwtTokenGenerator(IOptions<JwtSettings> optionsJwtSettings)
    {
        _jwtSettings = optionsJwtSettings.Value;
    }

    /// <inheritdoc/>
    public string GenerateToken(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.Now.AddDays(Convert.ToDouble(_jwtSettings.ExpireDays));

        var claims = new[]
        {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.Value.ToString()),
                new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
                new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
                new Claim(JwtRegisteredClaimNames.Jti, user.Id.Value.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
        };

        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            expires: expires,
            claims: claims,
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
    }
}
