
using FluentValidation.Results;
using TekChallenge.Services.AuthService.Application.Abstractions.Repositories;
using TekChallenge.Services.AuthService.Application.Users.Commands.AddUser;
using TekChallenge.Services.AuthService.Application.Users.Commands.DeleteUser;
using TekChallenge.Services.AuthService.Application.Users.Commands.UpdateUser;
using TekChallenge.Services.AuthService.Application.Users.Dtos;
using TekChallenge.Services.AuthService.Application.Users.Queries.GetUserById;
using TekChallenge.Services.AuthService.Application.Users.Queries.GetUsersList;
using TekChallenge.Services.AuthService.Contracts.Enums;
using TekChallenge.Services.AuthService.Domain.Users;
using TekChallenge.Services.AuthService.Domain.Users.ValueObjects;
using TekChallenge.SharedDefinitions.Domain.Common.Abstractions;

namespace TekChallenge.Tests.Services.AuthService.Application;

public class UsersCommandAndQueriesTests
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHashingService _passwordHashingService;

    public UsersCommandAndQueriesTests()
    {
        _userRepository = Substitute.For<IUserRepository>();
        _passwordHashingService = Substitute.For<IPasswordHashingService>();
    }

    [Fact]
    public async Task Handle_ValidRequest_ReturnUserDto()
    {
        // Arrange
        var creationDate = DateTime.Now;
        var userDto = new UserDto(1, "John", "Doe", "john.doe@example.com", "", 1, creationDate);
        var addUserCommand = new AddUserCommand("John", "Doe", "john.doe@example.com", "hashedPassword");
        var userDomainModel = User.Create(1, "John", "Doe", "john.doe@example.com", "hashedPassword", Roles.Manager, creationDate, passwordHasher: _passwordHashingService);
        _userRepository.AddAsync(Arg.Any<User>()).Returns(Result.Ok(userDomainModel.Value));

        var handler = new AddUserCommandHandler(_userRepository, _passwordHashingService);

        // Act
        var result = await handler.Handle(addUserCommand, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(userDto);
    }

    [Fact]
    public async Task Handle_InvalidRequest_ReturnError()
    {
        // Arrange
        var addUserCommand = new AddUserCommand("John", "Doe", "john.doe@example.com", "");
        var error = new Error("Invalid Password");
        _ = User.Create(null, "John", "Doe", "john.doe@example.com", "", passwordHasher: _passwordHashingService);
        _userRepository.AddAsync(Arg.Any<User>()).Returns(Result.Fail(error));
        var handler = new AddUserCommandHandler(_userRepository, _passwordHashingService);

        // Act
        var result = await handler.Handle(addUserCommand, CancellationToken.None);

        // Assert
        result.IsFailed.Should().BeTrue();
        result.Errors.Should().ContainEquivalentOf(error);
    }

    [Fact]
    public async Task Handle_ValidId_ReturnsSuccessResult()
    {
        // Arrange
        long userId = 1;
        var deleteCommand = new DeleteUserCommand(userId);
        var handler = new DeleteUserCommandHandler(_userRepository);
        _userRepository.RemoveAsync(Arg.Any<UserId>()).Returns(Result.Ok());

        // Act
        var result = await handler.Handle(deleteCommand, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_InvalidId_ReturnsFailureResult()
    {
        // Arrange
        long userId = 0;
        var deleteCommand = new DeleteUserCommand(userId);
        var handler = new DeleteUserCommandHandler(_userRepository);
        var error = new Error("User not found");
        _userRepository.RemoveAsync(Arg.Any<UserId>()).Returns(Result.Fail("User not found"));

        // Act
        var result = await handler.Handle(deleteCommand, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().ContainEquivalentOf(error);
    }

    [Fact]
    public async Task Handle_RepositoryException_ReturnsFailureResult()
    {
        // Arrange
        long userId = 2;
        var deleteCommand = new DeleteUserCommand(userId);
        var handler = new DeleteUserCommandHandler(_userRepository);
        var error = new Error("Repository exception");
        _userRepository.RemoveAsync(Arg.Any<UserId>()).Returns(Result.Fail("Repository exception"));

        // Act
        var result = await handler.Handle(deleteCommand, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().ContainEquivalentOf(error);
    }

    [Fact]
    public async Task Handle_ValidRequest_ReturnsUserDto()
    {
        // Arrange
        var handler = new UpdateUserCommandHandler(_userRepository, _passwordHashingService);
        var request = new UpdateUserCommand(1, "John", "Doe", "john.doe@example.com", "password123", (int)Roles.Admin);
        var userDomainModel = User.Create(1, "John", "Doe", "john.doe@example.com", "password123", Roles.Admin, passwordHasher: _passwordHashingService);

        _userRepository.UpdateAsync(Arg.Any<User>()).Returns(userDomainModel);

        // Act
        var result = await handler.Handle(request, default);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.Id.Should().Be(1);
        result.Value.FirstName.Should().Be("John");
        result.Value.LastName.Should().Be("Doe");
        result.Value.Email.Should().Be("john.doe@example.com");
        result.Value.Password.Should().Be("password123");
        result.Value.Role.Should().Be((int)Roles.Admin);
    }

    [Fact]
    public async Task Handle_InvalidRequest_ReturnsFailureResult()
    {
        // Arrange
        var handler = new UpdateUserCommandHandler(_userRepository, _passwordHashingService);
        var request = new UpdateUserCommand(1, "John", "Doe", "john.doe@example.com", "password123", (int)Roles.Admin);
        var userDomainModel = Result.Fail<User>("Invalid data.");

        _userRepository.UpdateAsync(Arg.Any<User>()).Returns(userDomainModel);

        // Act
        var result = await handler.Handle(request, default);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
    }

    [Fact]
    public async Task Handle_ValidId_ReturnsUserDto()
    {
        // Arrange
        var userId = 1;
        var utcNow = DateTime.UtcNow;
        var getUserByIdQuery = new GetUserByIdQuery(userId);
        var userDto = new UserDto(userId, "John", "Doe", "john.doe@example.com", "password123", 1, utcNow);
        _userRepository.GetByIdAsync(userId).Returns(Task.FromResult(Result.Ok(User.Create(userId, "John", "Doe", "john.doe@example.com", "password123", Roles.Manager, utcNow).Value)));
        var handler = new GetUserByIdQueryHandler(_userRepository);

        // Act
        var result = await handler.Handle(getUserByIdQuery, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(userDto);
    }

    [Fact]
    public async Task Handle_InvalidId_ReturnsErrorResult()
    {
        // Arrange
        var userId = 0;
        var getUserByIdQuery = new GetUserByIdQuery(userId);
        _userRepository.GetByIdAsync(userId).Returns(Result.Fail(new Error("User not found")));
        var handler = new GetUserByIdQueryHandler(_userRepository);

        // Act
        var result = await handler.Handle(getUserByIdQuery, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsFailed.Should().BeTrue();
        result.Errors.Should().ContainSingle().Which.Message.Should().Be("User not found");
    }

    [Fact]
    public void Validate_ValidId_NoValidationErrors()
    {
        // Arrange
        var getUserByIdQueryValidator = new GetUserByIdQueryValidator();
        var getUserByIdQuery = new GetUserByIdQuery(1);

        // Act
        var result = getUserByIdQueryValidator.Validate(getUserByIdQuery);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_WithValidData_ShouldReturnListOfUserDtos()
    {
        // Arrange
        var userRepository = Substitute.For<IUserRepository>();
        var usersListQuery = new GetUsersListQuery();
        var usersListQueryHandler = new GetUsersListQueryHandler(userRepository);

        var mockedUsers = new List<User>
        {
            User.Create(1, "John", "Doe", "john.doe@example.com", "password123", Roles.Admin, passwordHasher: _passwordHashingService).Value,
        };

        userRepository.GetAllAsync().Returns(Result.Ok(mockedUsers));

        // Act
        var result = await usersListQueryHandler.Handle(usersListQuery, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.Should().HaveCount(1);
        result.Value.First().FirstName.Should().Be("John");
        result.Value.First().LastName.Should().Be("Doe");
        result.Value.First().Email.Should().Be("john.doe@example.com");
        result.Value.First().Role.Should().Be((int)Roles.Admin);
    }

    [Fact]
    public async Task Handle_WithNoUsers_ShouldReturnEmptyList()
    {
        // Arrange
        var userRepository = Substitute.For<IUserRepository>();
        var usersListQuery = new GetUsersListQuery();
        var usersListQueryHandler = new GetUsersListQueryHandler(userRepository);

        userRepository.GetAllAsync().Returns(Result.Ok(new List<User>()));

        // Act
        var result = await usersListQueryHandler.Handle(usersListQuery, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.Should().BeEmpty();
    }
}