// <copyright file="Password.cs" company="Tek">
// Copyright (c) AuthService. All rights reserved.
// </copyright>


using TekChallenge.SharedDefinitions.Domain.Common.Abstractions;
using TekChallenge.SharedDefinitions.Domain.Common.DDD;

namespace TekChallenge.Services.AuthService.Domain.Users.ValueObjects;

/// <summary>
/// Password Value Object.
/// </summary>
public class Password : ValueObject
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Password"/> class.
    /// </summary>
    /// <param name="plainTextPassword">The password string value.</param>
    /// <param name="hasher">the IPasswordHashingService instance.</param>
    public Password(string plainTextPassword, IPasswordHashingService? hasher = null)
    {
        if (string.IsNullOrEmpty(plainTextPassword))
        {
            throw new ArgumentNullException(nameof(plainTextPassword), "Password cannot be null or empty");
        }

        Value = plainTextPassword;

        HashedPassword = hasher?.HashPassword(plainTextPassword) ?? plainTextPassword;
    }

    /// <summary>
    /// Gets Plain Text Password Value.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Gets a value indicating whether the Value is Hashed.
    /// </summary>
    public bool IsHashed => Value == HashedPassword;

    /// <summary>
    /// Gets the Hashed Password.
    /// </summary>
    public string HashedPassword { get; private set; }

    /// <summary>
    /// Validates the informed password agains the existing password.
    /// </summary>
    /// <param name="password">The new password string value to validate.</param>
    /// <param name="hasher">The IPasswordHashingService instace.</param>
    /// <returns>True if passwords match.</returns>
    public bool Verify(string password, IPasswordHashingService hasher)
    {
        if (string.IsNullOrEmpty(password))
        {
            return false;
        }

        return hasher.HashPassword(password) == HashedPassword;
    }

    /// <inheritdoc/>
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
        yield return HashedPassword;
    }

    /// <summary>
    /// Perform Password Hashing logic on demand.
    /// </summary>
    /// <param name="passwordHasher">The PasswordHasher Instance.</param>
    public void HashPassword(IPasswordHashingService passwordHasher)
    {
        HashedPassword = passwordHasher.HashPassword(Value);
    }
}
