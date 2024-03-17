// <copyright file="RedisCache.cs" company="Tek">
// Copyright (c) TekChallenge.SharedDefinitions. All rights reserved.
// </copyright>

using Dapr.Client;
using Microsoft.Extensions.Logging;
using TekChallenge.SharedDefinitions.Application.Abstractions.Services;

namespace SharedDefinitions.Infrastructure.Services;

/// <summary>
/// Cache implementation through Dapr State Managent over Redis Cache.
/// </summary>
public class DaprCacheService : ICacheService
{
    private const string STATE_STORE_NAME = "state-store";

    private readonly DaprClient _daprClient;
    private readonly ILogger _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="DaprCacheService"/> class.
    /// </summary>
    /// <param name="daprClient">DaprClient being injected.</param>
    /// <param name="logger">ILogger being injected.</param>
    public DaprCacheService(DaprClient daprClient, ILogger<DaprCacheService> logger)
    {
        _daprClient = daprClient;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default) 
    {
        _logger.LogInformation($"DaprCache.Get({key})");

        try
        {
            return await _daprClient.GetStateAsync<T>(STATE_STORE_NAME, key, cancellationToken: cancellationToken);
        }
        catch (Exception)
        {
            return default;
        }
    }

    /// <inheritdoc/>
    public async Task<bool> SetAsync<T>(string key, T value, CancellationToken cancellationToken = default) 
    {
        _logger.LogInformation($"DaprCache.Set({key})");
        return await SetAsync<T>(key, value, TimeSpan.FromMinutes(5));
    }

    /// <inheritdoc/>
    public async Task<bool> SetAsync<T>(string key, T value, TimeSpan timeToLive, CancellationToken cancellationToken = default) 
    {
        _logger.LogInformation($"DaprCache.SetTtl({key})");

        try
        {
            await _daprClient.SaveStateAsync(
                STATE_STORE_NAME, 
                key, 
                value,
                metadata: new Dictionary<string, string>() {
                    {
                        "ttlInSeconds", timeToLive.TotalSeconds.ToString()
                    }
                },
                cancellationToken: cancellationToken);

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <inheritdoc/>
    public async Task<bool> RemoveAsync<T>(string key, CancellationToken cancellationToken = default) 
    {
        _logger.LogInformation($"DaprCache.Remove({key})");

        try
        {
            await _daprClient.DeleteStateAsync(STATE_STORE_NAME, key, cancellationToken: cancellationToken);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
