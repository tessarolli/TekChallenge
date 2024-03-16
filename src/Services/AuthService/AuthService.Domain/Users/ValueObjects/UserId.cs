// <copyright file="UserId.cs" company="Tek">
// Copyright (c) AuthService. All rights reserved.
// </copyright>

using TekChallenge.SharedDefinitions.Domain.Common.DDD;

namespace TekChallenge.Services.AuthService.Domain.Users.ValueObjects;

/// <summary>
/// User Id Value Object.
/// </summary>
public sealed class UserId : ValueObject
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UserId"/> class.
    /// </summary>
    /// <param name="id">Id value if exists.</param>
    public UserId(long? id = null)
    {
        if (id is null)
        {
            Value = 0;
        }
        else
        {
            Value = id.Value;
        }
    }

    /// <summary>
    /// Gets the User ID.
    /// </summary>
    public long Value { get; }

    /// <summary>
    /// Method required for comparing value objects.
    /// </summary>
    /// <returns>An ienumerable with all the properties of the value object.</returns>
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
