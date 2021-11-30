using FluentAssertions;
using Xunit;

namespace ThomasW.Domain.SharedKernel.Results.UnitTests;

public sealed class PendingResultTests
{
    public sealed class Success
    {
        [Fact]
        public void Success_Value_ReturnsSuccessfulResult()
        {
            // Arrange
            PendingResult<string> pendingResult = Result.OfType<string>();

            // Act
            Result<string> result = pendingResult.Success("Test Value");

            // Assert
            result.IsSuccessful.Should().BeTrue();
            result.Value.Should().Be("Test Value");
            result.IsFailed.Should().BeFalse();
            result.FailureReason.Should().BeNull();
        }
    }

    public sealed class Fail
    {
        [Fact]
        public void Fail_ValueType_ReturnsFailedResult()
        {
            // Arrange
            PendingResult<string> pendingResult = Result.OfType<string>();
            TestFailureReason failureReason = new();

            // Act
            Result<string> result = pendingResult.Fail(failureReason);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.FailureReason.Should().Be(failureReason);
            result.IsSuccessful.Should().BeFalse();
            result.Value.Should().BeNull();
        }

        private sealed class TestFailureReason : FailureReason
        {
        }
    }
}
