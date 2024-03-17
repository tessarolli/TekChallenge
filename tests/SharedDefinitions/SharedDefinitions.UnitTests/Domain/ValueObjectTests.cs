using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TekChallenge.SharedDefinitions.Domain.Common.DDD;

namespace TekChallenge.Tests.SharedDefinitions.Domain;

public class ValueObjectTests
{
    // Test case to verify equality between two identical value objects
    [Fact]
    public void TwoIdenticalValueObjects_ShouldBeEqual()
    {
        // Arrange
        var utcNow = DateTime.UtcNow;
        var valueObject1 = Substitute.ForPartsOf<ValueObject>();
        valueObject1.GetEqualityComponents().Returns(new List<object> { 1, "test", utcNow });

        var valueObject2 = Substitute.ForPartsOf<ValueObject>();
        valueObject2.GetEqualityComponents().Returns(new List<object> { 1, "test", utcNow });

        // Act
        var result = valueObject1.Equals(valueObject2);

        // Assert
        result.Should().BeTrue();
    }

    // Test case to verify inequality between two different value objects
    [Fact]
    public void TwoDifferentValueObjects_ShouldNotBeEqual()
    {
        // Arrange
        var utcNow = DateTime.UtcNow;
        var valueObject1 = Substitute.ForPartsOf<ValueObject>();
        valueObject1.GetEqualityComponents().Returns(new List<object> { 1, "test1", utcNow });

        var valueObject2 = Substitute.ForPartsOf<ValueObject>();
        valueObject2.GetEqualityComponents().Returns(new List<object> { 1, "test2", utcNow });

        // Act
        var result = valueObject1.Equals(valueObject2);

        // Assert
        result.Should().BeFalse();
    }

    // Test case to verify that GetHashCode returns the same value for equal value objects
    [Fact]
    public void GetHashCode_EqualValueObjects_ShouldReturnSameHashCode()
    {
        // Arrange
        var utcNow = DateTime.UtcNow;
        var valueObject1 = Substitute.ForPartsOf<ValueObject>();
        valueObject1.GetEqualityComponents().Returns(new List<object> { 1, "test", utcNow });

        var valueObject2 = Substitute.ForPartsOf<ValueObject>();
        valueObject2.GetEqualityComponents().Returns(new List<object> { 1, "test", utcNow });

        // Act
        var hashCode1 = valueObject1.GetHashCode();
        var hashCode2 = valueObject2.GetHashCode();

        // Assert
        hashCode1.Should().Be(hashCode2);
    }

    // Test case to verify that GetHashCode returns different values for different value objects
    [Fact]
    public void GetHashCode_DifferentValueObjects_ShouldReturnDifferentHashCode()
    {
        // Arrange
        var utcNow = DateTime.UtcNow;
        var valueObject1 = Substitute.ForPartsOf<ValueObject>();
        valueObject1.GetEqualityComponents().Returns(new List<object> { 1, "test1", utcNow });

        var valueObject2 = Substitute.ForPartsOf<ValueObject>();
        valueObject2.GetEqualityComponents().Returns(new List<object> { 1, "test2", utcNow });

        // Act
        var hashCode1 = valueObject1.GetHashCode();
        var hashCode2 = valueObject2.GetHashCode();

        // Assert
        hashCode1.Should().NotBe(hashCode2);
    }
}