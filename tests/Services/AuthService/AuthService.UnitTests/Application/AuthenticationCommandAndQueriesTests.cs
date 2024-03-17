using TekChallenge.Services.AuthService.Application.Abstractions.Authentication;
using TekChallenge.Services.AuthService.Application.Abstractions.Repositories;
using TekChallenge.Services.AuthService.Application.Authentication.Commands.Register;
using TekChallenge.Services.AuthService.Application.Authentication.Errors;
using TekChallenge.Services.AuthService.Application.Authentication.Queries.Login;
using TekChallenge.Services.AuthService.Domain.Users;
using TekChallenge.SharedDefinitions.Application.Common.Errors;
using TekChallenge.SharedDefinitions.Domain.Common.Abstractions;

namespace TekChallenge.Tests.Services.AuthService;

public class AuthenticationCommandAndQueriesTests
{
    [Fact]
    public async Task Handle_UserDoesNotExist_SuccessfullyRegistersUser()
    {
        // Arrange
        var userRepository = Substitute.For<IUserRepository>();
        var jwtTokenGenerator = Substitute.For<IJwtTokenGenerator>();
        var passwordHashingService = Substitute.For<IPasswordHashingService>();
        var registerCommandHandler = new RegisterCommandHandler(userRepository, jwtTokenGenerator, passwordHashingService);
        var registerCommand = new RegisterCommand("John", "Doe", "test@example.com", "Password123");
        var user = User.Create(null, registerCommand.FirstName, registerCommand.LastName, registerCommand.Email, registerCommand.Password, passwordHasher: passwordHashingService).Value;
        userRepository.GetByEmailAsync(registerCommand.Email).Returns(Result.Fail(new NotFoundError()));
        userRepository.AddAsync(Arg.Any<User>()).Returns(Result.Ok(user));
        jwtTokenGenerator.GenerateToken(Arg.Any<User>()).Returns("token");

        // Act
        var result = await registerCommandHandler.Handle(registerCommand, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.User.Should().Be(user);
        result.Value.Token.Should().Be("token");
    }

    [Fact]
    public async Task Handle_UserAlreadyExists_ReturnsError()
    {
        // Arrange
        var userRepository = Substitute.For<IUserRepository>();
        var jwtTokenGenerator = Substitute.For<IJwtTokenGenerator>();
        var passwordHashingService = Substitute.For<IPasswordHashingService>();
        var registerCommandHandler = new RegisterCommandHandler(userRepository, jwtTokenGenerator, passwordHashingService);
        var registerCommand = new RegisterCommand("John", "Doe", "test@example.com", "Password123");
        userRepository.GetByEmailAsync(registerCommand.Email).Returns(Result.Fail(new UserWithEmailAlreadyExistsError()));

        // Act
        var result = await registerCommandHandler.Handle(registerCommand, CancellationToken.None);

        // Assert
        result.IsFailed.Should().BeTrue();
        result.Errors.Should().Contain(error => error.GetType() == typeof(UserWithEmailAlreadyExistsError));
    }

    [Fact]
    public async Task Handle_WhenUserExistsAndValidPassword_ReturnsAuthenticationResult()
    {
        // Arrange
        var userRepository = Substitute.For<IUserRepository>();
        var jwtTokenGenerator = Substitute.For<IJwtTokenGenerator>();
        var passwordHasher = Substitute.For<IPasswordHashingService>();
        var query = new LoginQuery("user@example.com", "Password123");
        var user = User.Create(1, "John", "Doe", "user@example.com", "StrongPassword");
        userRepository.GetByEmailAsync(Arg.Any<string>()).Returns(Result.Ok(user.Value));
        passwordHasher.VerifyPassword(query.Password, user.Value.Password.Value).Returns(true);
        var handler = new LoginQueryHandler(userRepository, jwtTokenGenerator, passwordHasher);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.User.Should().Be(user.Value);
    }

    [Fact]
    public async Task Handle_WhenUserDoesNotExist_ReturnsFailure()
    {
        // Arrange
        var userRepository = Substitute.For<IUserRepository>();
        var jwtTokenGenerator = Substitute.For<IJwtTokenGenerator>();
        var passwordHasher = Substitute.For<IPasswordHashingService>();
        var query = new LoginQuery("user@example.com", "Password123");
        userRepository.GetByEmailAsync(Arg.Any<string>()).Returns(Result.Fail(new Error("User not found")));
        var handler = new LoginQueryHandler(userRepository, jwtTokenGenerator, passwordHasher);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsFailed.Should().BeTrue();
        result.Errors.Should().ContainSingle().Which.Message.Should().Be("User not found");
    }

    [Fact]
    public async Task Handle_WhenInvalidPassword_ReturnsInvalidPasswordError()
    {
        // Arrange
        var userRepository = Substitute.For<IUserRepository>();
        var jwtTokenGenerator = Substitute.For<IJwtTokenGenerator>();
        var passwordHasher = Substitute.For<IPasswordHashingService>();
        var query = new LoginQuery("user@example.com", "InvalidPassword");
        var user = User.Create(1, "John", "Doe", "user@example.com", "StrongPassword");
        userRepository.GetByEmailAsync(Arg.Any<string>()).Returns(Result.Ok(user.Value));
        passwordHasher.VerifyPassword(query.Password, user.Value.Password.Value).Returns(false);
        var handler = new LoginQueryHandler(userRepository, jwtTokenGenerator, passwordHasher);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsFailed.Should().BeTrue();
        result.Errors.Should().ContainSingle().Which.Should().BeOfType<InvalidPasswordError>();
    }
}