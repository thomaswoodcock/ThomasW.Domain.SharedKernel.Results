using FluentAssertions;

using ThomasW.Domain.SharedKernel.Results.FluentAssertions;

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
        result.Should().BeSuccessful();
    }

    [Fact]
    public void Success_Value_ReturnsSuccessfulResult()
    {
        // Arrange Act
        var result = Result.Success("Test Value");

        // Assert
        result.Should().BeSuccessful()
            .And.BeSuccessful(value => value == "Test Value");
    }

    [Fact]
    public void Fail_NoValueType_ReturnsFailedResult()
    {
        // Arrange
        TestFailureReason failureReason = new("Test Message");

        // Act
        var result = Result.Fail(failureReason);

        // Assert
        result.Should().BeFailed()
            .And.BeFailed<TestFailureReason>()
            .And.BeFailed<TestFailureReason>(reason => reason.Message == "Test Message");
    }

    [Fact]
    public void Fail_ValueType_ReturnsFailedResult()
    {
        // Arrange
        TestFailureReason failureReason = new("Test Message");

        // Act
        var result = Result.Fail<string>(failureReason);

        // Assert
        result.Should().BeFailed()
            .And.BeFailed<TestFailureReason>()
            .And.BeFailed<TestFailureReason>(reason => reason.Message == "Test Message");
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
        public TestFailureReason(string message)
        {
            this.Message = message;
        }

        public string Message { get; }
    }
}
