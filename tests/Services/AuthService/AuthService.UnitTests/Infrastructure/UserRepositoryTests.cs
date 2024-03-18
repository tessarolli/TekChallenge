using Microsoft.Extensions.Logging;
using System.Data;
using TekChallenge.Services.AuthService.Contracts.Enums;
using TekChallenge.Services.AuthService.Domain.Users.ValueObjects;
using TekChallenge.Services.AuthService.Domain.Users;
using TekChallenge.SharedDefinitions.Domain.Common.Abstractions;
using TekChallenge.Services.AuthService.Application.Abstractions.Repositories;
using TekChallenge.SharedDefinitions.Infrastructure.Abstractions;
using TekChallenge.Services.AuthService.Infrastructure.Repositories;
using TekChallenge.Services.AuthService.Infrastructure.Dtos;
using System.Data.Common;

namespace TekChallenge.Tests.Services.AuthService.Infrastructure;

public class UserRepositoryTests
{
    private readonly IUserRepository _userRepository;
    private readonly IDapperUtility _dapper;

    public UserRepositoryTests()
    {
        _dapper = Substitute.For<IDapperUtility>();
        var connectionFactory = Substitute.For<ISqlConnectionFactory<DbConnection>>();
        var logger = Substitute.For<ILogger<UserRepository>>();
        var passwordHasher = Substitute.For<IPasswordHashingService>();
        _userRepository = new UserRepository(_dapper, connectionFactory, logger, passwordHasher);
    }

    [Fact]
    public async Task GetByIdAsync_ValidId_ReturnsUser()
    {
        // Arrange
        var expectedUser = new UserDb(1, "John", "Doe", "john.doe@example.com", "hashedPassword", (int)Roles.Admin, DateTime.UtcNow);
        _dapper.QueryFirstOrDefaultAsync<UserDb>(Arg.Any<string>(), Arg.Any<object>(), Arg.Any<CommandType>(), Arg.Any<DbTransaction?>()).Returns(expectedUser);

        // Act
        var result = await _userRepository.GetByIdAsync(1);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.Value.Id.Value.Should().Be(1L);
        result.Value.FirstName.Should().Be("John");
        result.Value.LastName.Should().Be("Doe");
        result.Value.Email.Should().Be("john.doe@example.com");
        result.Value.Role.Should().Be(Roles.Admin);
    }

    
    [Fact]
    public async Task GetByIdsAsync_ValidIds_ReturnsListOfUsers()
    {
        // Arrange
        var expectedUsers = new List<UserDb>
        {
            new UserDb(1, "John", "Doe", "john.doe@example.com", "hashedPassword", (int)Roles.Admin, DateTime.UtcNow),
            new UserDb(2, "Jane", "Smith", "jane.smith@example.com", "hashedPassword", (int)Roles.User, DateTime.UtcNow)
        };
        _dapper.QueryAsync<UserDb>(Arg.Any<string>(), Arg.Any<object>(), Arg.Any<CommandType>(), Arg.Any<DbTransaction?>()).Returns(expectedUsers);

        // Act
        var result = await _userRepository.GetByIdsAsync(new long[] { 1, 2 });

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(2);
        result[0].Id.Value.Should().Be(1);
        result[1].Id.Value.Should().Be(2);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsListOfUsers()
    {
        // Arrange
        var expectedUsers = new List<UserDb>
        {
            new UserDb(1, "John", "Doe", "john.doe@example.com", "hashedPassword", (int)Roles.Admin, DateTime.UtcNow),
            new UserDb(2, "Jane", "Smith", "jane.smith@example.com", "hashedPassword", (int)Roles.User, DateTime.UtcNow)
        };

        _dapper.QueryAsync<UserDb>(Arg.Any<string>(), Arg.Any<object>(), Arg.Any<CommandType>(), Arg.Any<DbTransaction?>()).Returns(expectedUsers);

        // Act
        var result = await _userRepository.GetAllAsync();

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().Be(true);
        result.Value.Should().HaveCount(2);
        result.Value[0].Id.Value.Should().Be(1);
        result.Value[1].Id.Value.Should().Be(2);
    }

    [Fact]
    public async Task GetByEmailAsync_ValidEmail_ReturnsUser()
    {
        // Arrange
        var expectedUser = new UserDb(1, "John", "Doe", "john.doe@example.com", "hashedPassword", (int)Roles.Admin, DateTime.UtcNow);
        _dapper.QueryFirstOrDefaultAsync<UserDb>(Arg.Any<string>(), Arg.Any<object>(), Arg.Any<CommandType>(), Arg.Any<DbTransaction?>()).Returns(expectedUser);

        // Act
        var result = await _userRepository.GetByEmailAsync("john.doe@example.com");

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.Value.Email.Should().Be("john.doe@example.com");
    }

    [Fact]
    public async Task AddAsync_ValidUser_ReturnsUser()
    {
        // Arrange
        var utcNow = DateTime.UtcNow;
        var user = User.Create(1, "John", "Doe", "john.doe@example.com", "hashedPassword", Roles.Admin, utcNow);
        _dapper.ExecuteScalarAsync(Arg.Any<string>(), Arg.Any<object>()).Returns(1);
        _dapper.QueryFirstOrDefaultAsync<UserDb>(Arg.Any<string>(), Arg.Any<object>()).Returns(new UserDb(1, "John", "Doe", "john.doe@example.com", "hashedPassword", (int)Roles.Admin, utcNow));

        // Act
        var result = await _userRepository.AddAsync(user.Value);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.Value.Id.Value.Should().Be(1);
        result.Value.FirstName.Should().Be("John");
        result.Value.LastName.Should().Be("Doe");
        result.Value.Email.Should().Be("john.doe@example.com");
        result.Value.Role.Should().Be(Roles.Admin);
    }

    [Fact]
    public async Task UpdateAsync_ValidUser_ReturnsUser()
    {
        // Arrange
        var utcNow = DateTime.UtcNow;
        var user = User.Create(1, "John", "Doe", "john.doe@example.com", "hashedPassword", Roles.Admin, utcNow);
        _dapper.ExecuteAsync(Arg.Any<string>(), Arg.Any<object>()).Returns(1);

        // Act
        var result = await _userRepository.UpdateAsync(user.Value);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.Value.FirstName.Should().Be("John");
        result.Value.LastName.Should().Be("Doe");
        result.Value.Email.Should().Be("john.doe@example.com");
        result.Value.Role.Should().Be(Roles.Admin);
    }

    [Fact]
    public async Task RemoveAsync_ValidUserId_ReturnsSuccessResult()
    {
        // Arrange
        var userId = new UserId(1);
        _dapper.ExecuteAsync(Arg.Any<string>(), Arg.Any<object>()).Returns(1);

        // Act
        var result = await _userRepository.RemoveAsync(userId);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
    }
}