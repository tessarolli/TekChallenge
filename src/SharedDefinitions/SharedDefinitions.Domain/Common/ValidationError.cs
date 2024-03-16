// <copyright file="ValidationError.cs" company="Tek">
// Copyright (c) TekChallenge.SharedDefinitions. All rights reserved.
// </copyright>

using FluentResults;

namespace TekChallenge.SharedDefinitions.Domain.Common;

/// <summary>
/// Base class to identify that an Validation Error ocurred.
/// </summary>
public class ValidationError : Error
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationError"/> class.
    /// </summary>
    /// <param name="message">Validation Error Message.</param>
    /// <param name="causedBy">Caused By.</param>
    public ValidationError(string message, IError causedBy)
        : base(message, causedBy)
    {
    }
}
