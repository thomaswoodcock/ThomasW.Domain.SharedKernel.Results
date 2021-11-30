namespace ThomasW.Domain.SharedKernel.Results;

/// <summary>
///     Represents a pending result that may or may not return a value.
/// </summary>
/// <typeparam name="T">
///     The type of the value.
/// </typeparam>
public sealed class PendingResult<T>
    where T : notnull
{
    internal PendingResult()
    {
    }

    /// <summary>
    ///     Creates a successful result from the pending result with a given <paramref name="value" />.
    /// </summary>
    /// <param name="value">
    ///     The value returned by the operation.
    /// </param>
    /// <returns>
    ///     A <see cref="Result{T}" /> indicating that an operation was successful and returned a <paramref name="value" />.
    /// </returns>
    public Result<T> Success(T value)
    {
        return Result.Success(value);
    }

    /// <summary>
    ///     Creates a failed result from the pending result that would have contained a value had the operation been
    ///     successful.
    /// </summary>
    /// <param name="reason">
    ///     The reason that the operation failed.
    /// </param>
    /// <returns>
    ///     A <see cref="Result{T}" /> indicating that an operation failed for a given reason and did not return a value.
    /// </returns>
    public Result<T> Fail(FailureReason reason)
    {
        return Result.Fail<T>(reason);
    }
}
