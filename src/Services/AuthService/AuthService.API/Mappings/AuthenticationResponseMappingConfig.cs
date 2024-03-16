// <copyright file="AuthenticationResponseMappingConfig.cs" company="Tek">
// Copyright (c) AuthService. All rights reserved.
// </copyright>

using Mapster;
using TekChallenge.Services.AuthService.Application.Authentication.Results;
using TekChallenge.Services.AuthService.Contracts.Authentication;

namespace TekChallenge.Services.AuthService.API.Mappings;

/// <summary>
/// Authentication Mapster Config Mapping.
/// </summary>
public class AuthenticationResponseMappingConfig : IRegister
{
    /// <inheritdoc/>
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<AuthenticationResult, AuthenticationResponse>()
            .Map(dest => dest.Token, src => src.Token)
            .Map(dest => dest.Id, src => src.User.Id.Value)
            .Map(dest => dest, src => src.User);
    }
}
