// <copyright file="UnreachableExternalServiceException.cs" company="Tek">
// Copyright (c) TekChallenge.SharedDefinitions. All rights reserved.
// </copyright>

namespace SharedDefinitions.Domain.Common.Exceptions;

/// <summary>
/// Exception thrown when an external service cant be accessed.
/// </summary>
public class UnreachableExternalServiceException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UnreachableExternalServiceException"/> class.
    /// </summary>
    /// <param name="serviceName">The external service Name.</param>
    public UnreachableExternalServiceException(string serviceName = "") : base(serviceName)
    {
    }
}
