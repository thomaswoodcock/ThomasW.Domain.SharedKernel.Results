using FluentAssertions;

using Xunit;

namespace ThomasW.Domain.SharedKernel.Results.UnitTests;

public sealed class ResultTests
{
    [Fact]
    public void Success_NoValue_ReturnsSuccessfulResult()
    {
        // Arrange Act
        var result = Result.Success();

        // Assert
        result.IsSuccessful.Should().BeTrue();
        result.IsFailed.Should().BeFalse();
        result.FailureReason.Should().BeNull();
    }

    [Fact]
    public void Success_Value_ReturnsSuccessfulResult()
    {
        // Arrange Act
        var result = Result.Success("Test Value");

        // Assert
        result.IsSuccessful.Should().BeTrue();
        result.Value.Should().Be("Test Value");
        result.IsFailed.Should().BeFalse();
        result.FailureReason.Should().BeNull();
    }

    [Fact]
    public void Fail_NoValueType_ReturnsFailedResult()
    {
        // Arrange
        TestFailureReason failureReason = new();

        // Act
        var result = Result.Fail(failureReason);

        // Assert
        result.IsFailed.Should().BeTrue();
        result.FailureReason.Should().Be(failureReason);
        result.IsSuccessful.Should().BeFalse();
    }

    [Fact]
    public void Fail_ValueType_ReturnsFailedResult()
    {
        // Arrange
        TestFailureReason failureReason = new();

        // Act
        var result = Result.Fail<string>(failureReason);

        // Assert
        result.IsFailed.Should().BeTrue();
        result.FailureReason.Should().Be(failureReason);
        result.IsSuccessful.Should().BeFalse();
        result.Value.Should().BeNull();
    }

    [Fact]
    public void Pending_TypeValue_ReturnsPendingResult()
    {
        // Arrange Act
        PendingResult<string> result = Result.Pending<string>();

        // Assert
        result.Should().BeOfType<PendingResult<string>>();
    }

    private sealed class TestFailureReason : FailureReason
    {
    }
}
