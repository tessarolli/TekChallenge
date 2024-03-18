using TekChallenge.SharedDefinitions.Infrastructure.Services;

namespace TekChallenge.Tests.SharedDefinitions.Infrastructure.Services;

public class PasswordHashingServiceTests
{
    [Fact]
    public void HashPassword_ValidPassword_ReturnsHashedPassword()
    {
        // Arrange
        var password = "StrongPassword";
        var service = new PasswordHashingService();

        // Act
        var hashedPassword = service.HashPassword(password);

        // Assert
        hashedPassword.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public void VerifyPassword_CorrectPassword_ReturnsTrue()
    {
        // Arrange
        var password = "StrongPassword";
        var hashedPassword = "AQAAAAIAAYagAAAAEPxg7Fg5X57VJPEOYmcjQg4ptCS/v6yhn/2ywimXsN2UqTXaDDn4oGFnl3Yv+XjSVQ==";
        var service = new PasswordHashingService();

        // Act
        var result = service.VerifyPassword(password, hashedPassword);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void VerifyPassword_IncorrectPassword_ReturnsFalse()
    {
        // Arrange
        var incorrectPassword = "WrongPassword";
        var hashedPassword = "AQAAAAIAAYagAAAAEPxg7Fg5X57VJPEOYmcjQg4ptCS/v6yhn/2ywimXsN2UqTXaDDn4oGFnl3Yv+XjSVQ==";
        var service = new PasswordHashingService();

        // Act
        var result = service.VerifyPassword(incorrectPassword, hashedPassword);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void VerifyPassword_RehashedPassword_ReturnsTrue()
    {
        // Arrange
        var password = "StrongPassword";
        var service = new PasswordHashingService();

        // Act
        var rehashedPassword = service.HashPassword(password, 12000);
        var result = service.VerifyPassword(password, rehashedPassword);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void VerifyPassword_NullPassword_ReturnsFalse()
    {
        // Arrange
        var hashedPassword = "AQAAAAIAAYagAAAAEPxg7Fg5X57VJPEOYmcjQg4ptCS/v6yhn/2ywimXsN2UqTXaDDn4oGFnl3Yv+XjSVQ==";
        var service = new PasswordHashingService();

        // Act
        var result = service.VerifyPassword(string.Empty, hashedPassword);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void VerifyPassword_EmptyHashedPassword_ReturnsFalse()
    {
        // Arrange
        var password = "StrongPassword";
        var service = new PasswordHashingService();

        // Act
        var result = service.VerifyPassword(password, string.Empty);

        // Assert
        result.Should().BeFalse();
    }
}