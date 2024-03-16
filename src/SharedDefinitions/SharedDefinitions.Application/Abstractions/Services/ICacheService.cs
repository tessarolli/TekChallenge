// <copyright file="ICacheService.cs" company="Tek">
// Copyright (c) TekChallenge.SharedDefinitions. All rights reserved.
// </copyright>

using System;
using System.Threading;
using System.Threading.Tasks;

namespace TekChallenge.SharedDefinitions.Application.Abstractions.Services;

/// <summary>
/// Cache Service Interface.
/// </summary>
public interface ICacheService
{
    /// <summary>
    /// Tries to Get an existing cached instance of type <typeparamref name="T"/> for the given <paramref name="key"/>.
    /// </summary>
    /// <typeparam name="T">The Type you are trying to get.</typeparam>
    /// <param name="key">The key you are looking for.</param>
    /// <param name="cancellationToken">The Cancellation Token.</param>
    /// <returns>The Instance of T if found, default if not found.</returns>
    Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default);

    /// <summary>
    /// Tries to cache instance of <typeparamref name="T"/> in the typed cached for the given <paramref name="key"/>.
    /// </summary>
    /// <typeparam name="T">The Type you are trying to cache.</typeparam>
    /// <param name="key">The Key for this instance.</param>
    /// <param name="value">The instance of <typeparamref name="T"/>.</param>
    /// <param name="cancellationToken">The Cancellation Token.</param>
    /// <returns>True if cached, otherwise false.</returns>
    Task<bool> SetAsync<T>(string key, T value, CancellationToken cancellationToken = default);

    /// <summary>
    /// Tries to cache instance of <typeparamref name="T"/> in the typed cached for the given <paramref name="key"/>.
    /// </summary>
    /// <typeparam name="T">The Type you are trying to cache.</typeparam>
    /// <param name="key">The Key for this instance.</param>
    /// <param name="value">The instance of <typeparamref name="T"/>.</param>
    /// <param name="timeToLive">For how long this cached instance will be valid.</param>
    /// <param name="cancellationToken">The Cancellation Token.</param>
    /// <returns>True if cached, otherwise false.</returns>
    Task<bool> SetAsync<T>(string key, T value, TimeSpan timeToLive, CancellationToken cancellationToken = default);

    /// <summary>
    /// Tries to remove an instance of <typeparamref name="T"/> from the typed cache for the given <paramref name="key"/>.
    /// </summary>
    /// <typeparam name="T">The Type you are trying to remove from cache.</typeparam>
    /// <param name="key">The Key for this instance.</param>
    /// <param name="cancellationToken">The Cancellation Token.</param>
    /// <returns>True if removed, otherwise false.</returns>
    Task<bool> RemoveAsync<T>(string key, CancellationToken cancellationToken = default);
}
