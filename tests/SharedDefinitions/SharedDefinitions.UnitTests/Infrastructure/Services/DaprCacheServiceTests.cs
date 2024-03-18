using Dapr.Client;
using Microsoft.Extensions.Logging;
using SharedDefinitions.Infrastructure.Services;
using TekChallenge.SharedDefinitions.Application.Abstractions.Services;

namespace TekChallenge.Tests.SharedDefinitions.Infrastructure.Services;

public class DaprCacheServiceTests
{
    private readonly DaprClient _daprClient;
    private readonly ICacheService _cacheService;

    public DaprCacheServiceTests()
    {
        _daprClient = Substitute.For<DaprClient>();
        var logger = Substitute.For<ILogger<DaprCacheService>>();
        _cacheService = new DaprCacheService(_daprClient, logger);
    }

    [Fact]
    public async Task GetAsync_WithValidKey_ShouldReturnCachedValue()
    {
        // Arrange
        string key = "testKey";
        var expectedValue = "testValue";
        _daprClient.GetStateAsync<string>(Arg.Any<string>(), key, cancellationToken: Arg.Any<CancellationToken>()).Returns(expectedValue);

        // Act
        var result = await _cacheService.GetAsync<string>(key);

        // Assert
        result.Should().Be(expectedValue);
    }

    [Fact]
    public async Task GetAsync_WithInvalidKey_ShouldReturnDefault()
    {
        // Arrange
        string key = "invalidKey";
        _daprClient.GetStateAsync<string?>(Arg.Any<string>(), key, cancellationToken: Arg.Any<CancellationToken>()).Returns(Task.FromResult<string?>(null));

        // Act
        var result = await _cacheService.GetAsync<string>(key);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task SetAsync_WithValidKeyAndValue_ShouldReturnTrue()
    {
        // Arrange
        string key = "testKey";
        string value = "testValue";

        // Act
        var result = await _cacheService.SetAsync(key, value);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task SetAsync_WithValidKeyAndValueAndTimeToLive_ShouldReturnTrue()
    {
        // Arrange
        string key = "testKey";
        string value = "testValue";
        TimeSpan timeToLive = TimeSpan.FromSeconds(10);

        // Act
        var result = await _cacheService.SetAsync(key, value, timeToLive);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task RemoveAsync_WithValidKey_ShouldReturnTrue()
    {
        // Arrange
        string key = "testKey";

        // Act
        var result = await _cacheService.RemoveAsync<string>(key);

        // Assert
        result.Should().BeTrue();
        await _daprClient.Received(1).DeleteStateAsync(Arg.Any<string>(), key, cancellationToken: Arg.Any<CancellationToken>());
    }
    }
