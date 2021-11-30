using FluentAssertions;
using Xunit;

namespace ThomasW.Domain.SharedKernel.Results.UnitTests;

public sealed class ResultTests
{
    public sealed class Success
    {
        [Fact]
        public void Success_NoValue_ReturnsSuccessfulResult()
        {
            // Arrange Act
            Result result = Result.Success();

            // Assert
            result.IsSuccessful.Should().BeTrue();
            result.IsFailed.Should().BeFalse();
            result.FailureReason.Should().BeNull();
        }

        [Fact]
        public void Success_Value_ReturnsSuccessfulResult()
        {
            // Arrange Act
            Result<string> result = Result.Success("Test Value");

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
        public void Fail_NoValueType_ReturnsFailedResult()
        {
            // Arrange Act
            Result result = Result.Fail<TestFailureReason>();

            // Assert
            result.IsFailed.Should().BeTrue();
            result.FailureReason.Should().BeOfType<TestFailureReason>();
            result.IsSuccessful.Should().BeFalse();
        }

        [Fact]
        public void Fail_ValueType_ReturnsFailedResult()
        {
            // Arrange Act
            Result<string> result = Result.Fail<string, TestFailureReason>();

            // Assert
            result.IsFailed.Should().BeTrue();
            result.FailureReason.Should().BeOfType<TestFailureReason>();
            result.IsSuccessful.Should().BeFalse();
            result.Value.Should().BeNull();
        }

        private sealed class TestFailureReason : FailureReason
        {
        }
    }

    public sealed class OfType
    {
        [Fact]
        public void OfType_TypeValue_ReturnsPendingResult()
        {
            // Arrange Act
            PendingResult<string> result = Result.OfType<string>();

            // Assert
            result.Should().BeOfType<PendingResult<string>>();
        }
    }
}
