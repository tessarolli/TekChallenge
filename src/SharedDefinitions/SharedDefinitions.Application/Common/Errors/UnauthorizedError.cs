// <copyright file="UnauthorizedError.cs" company="Tek">
// Copyright (c) TekChallenge.SharedDefinitions. All rights reserved.
// </copyright>

using FluentResults;

namespace TekChallenge.SharedDefinitions.Application.Common.Errors;

/// <summary>
/// Unauthorized Error.
/// </summary>
public class UnauthorizedError : Error
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UnauthorizedError"/> class.
    /// </summary>
    /// <param name="message">Error message.</param>
    public UnauthorizedError(string message = "You do not have permission to access this resource.")
        : base(message)
    {
    }
}
