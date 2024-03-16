// <copyright file="IEntity.cs" company="Tek">
// Copyright (c) TekChallenge.SharedDefinitions. All rights reserved.
// </copyright>

namespace TekChallenge.SharedDefinitions.Domain.Common.Abstractions;

/// <summary>
/// IEntity interface.
/// </summary>
public interface IEntity
{
    /// <summary>
    /// Gets the Id of the Entity.
    /// </summary>
    /// <returns>Returns the Entity's Id Value Object.</returns>
    object GetId();
}
