using ThomasW.Domain.SharedKernel.Results.FluentAssertions;

using Xunit;

namespace ThomasW.Domain.SharedKernel.Results.UnitTests;

public class PendingResultTests
{
    [Fact]
    public void Success_Value_ReturnsSuccessfulResult()
    {
        // Arrange
        PendingResult<string> pendingResult = Result.Pending<string>();

        // Act
        Result<string> result = pendingResult.Success("Test Value");

        // Assert
        result.Should().BeSuccessful(value => value == "Test Value");
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
        result.Should().BeFailed<TestFailureReason>();
    }

    private sealed class TestFailureReason : FailureReason
    {
    }
}
