using FluentAssertions;

using Xunit;

namespace ThomasW.Domain.SharedKernel.Results.UnitTests;

public class PendingResultTests
{
    [Fact]
    public void Success_Value_ReturnsSuccessfulResult()
    {
        // Arrange
        object value = new();
        PendingResult<object> pendingResult = Result.Pending<object>();

        // Act
        Result<object> result = pendingResult.Success(value);

        // Assert
        result.Should().BeAssignableTo<Result>();
        result.IsSuccessful.Should().BeTrue();
        result.Value.Should().BeSameAs(value);
        result.IsFailed.Should().BeFalse();
        result.FailureReason.Should().BeNull();
    }

    [Fact]
    public void Fail_ValueType_ReturnsFailedResult()
    {
        // Arrange
        PendingResult<string> pendingResult = Result.Pending<string>();
        TestFailureReason failureReason = new();

        // Act
        Result<string> result = pendingResult.Fail(failureReason);

        // Assert
        result.Should().BeAssignableTo<Result>();
        result.IsFailed.Should().BeTrue();
        result.FailureReason.Should().BeSameAs(failureReason);
        result.IsSuccessful.Should().BeFalse();
        result.Value.Should().BeNull();
    }

    private sealed class TestFailureReason : FailureReason
    {
    }
}
