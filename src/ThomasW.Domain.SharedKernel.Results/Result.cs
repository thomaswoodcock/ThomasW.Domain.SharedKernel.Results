using System.Diagnostics.CodeAnalysis;

namespace ThomasW.Domain.SharedKernel.Results;

/// <summary>
///     Represents the result of an operation.
/// </summary>
public class Result
{
    private protected Result()
    {
    }

    private Result(FailureReason failureReason)
    {
        this.FailureReason = failureReason;
    }

    /// <summary>
    ///     Gets the reason that the operation failed.
    /// </summary>
    /// <remarks>
    ///     This will only have a value if <see cref="IsFailed" /> is <c>true</c>.
    /// </remarks>
    public virtual FailureReason? FailureReason { get; }

    /// <summary>
    ///     Gets a value indicating whether the operation failed.
    /// </summary>
    /// <remarks>
    ///     If this is <c>true</c>, <see cref="FailureReason" /> will have a value.
    /// </remarks>
    [MemberNotNullWhen(true, nameof(FailureReason))]
    public virtual bool IsFailed => this.FailureReason != null;

    /// <summary>
    ///     Gets a value indicating whether the operation was successful.
    /// </summary>
    /// <remarks>
    ///     If this is <c>false</c>, <see cref="FailureReason" /> will have a value.
    /// </remarks>
    [MemberNotNullWhen(false, nameof(FailureReason))]
    public virtual bool IsSuccessful => !this.IsFailed;

    /// <summary>
    ///     Creates a successful result.
    /// </summary>
    /// <returns>
    ///     A <see cref="Result" /> indicating that an operation was successful.
    /// </returns>
    public static Result Success()
    {
        return new Result();
    }

    /// <summary>
    ///     Creates a successful result that contains a value.
    /// </summary>
    /// <param name="value">
    ///     The value returned by the operation.
    /// </param>
    /// <typeparam name="T">
    ///     The type of the <paramref name="value" />.
    /// </typeparam>
    /// <returns>
    ///     A <see cref="Result{T}" /> indicating that an operation was successful and returned a <paramref name="value" />.
    /// </returns>
    public static Result<T> Success<T>(T value)
        where T : notnull
    {
        return new Result<T>(value);
    }

    /// <summary>
    ///     Creates a failed result.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of the failure reason.
    /// </typeparam>
    /// <returns>
    ///     A <see cref="Result" /> indicating that an operation failed for a given reason.
    /// </returns>
    public static Result Fail<T>()
        where T : FailureReason, new()
    {
        return new Result(new T());
    }

    /// <summary>
    ///     Creates a failed result that would have contained a value had the operation been successful.
    /// </summary>
    /// <typeparam name="TValue">
    ///     The type of the value.
    /// </typeparam>
    /// <typeparam name="TReason">
    ///     The type of the failure reason.
    /// </typeparam>
    /// <returns>
    ///     A <see cref="Result{T}" /> indicating that an operation failed for a given reason and did not return a value.
    /// </returns>
    public static Result<TValue> Fail<TValue, TReason>()
        where TValue : notnull
        where TReason : FailureReason, new()
    {
        return new Result<TValue>(new TReason());
    }

    /// <summary>
    ///     Creates a pending result that may or may not return a value.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of the value.
    /// </typeparam>
    /// <returns>
    ///     A <see cref="PendingResult{T}" /> indicating that the result of the operation is yet to be determined.
    /// </returns>
    public static PendingResult<T> OfType<T>()
        where T : notnull
    {
        return new PendingResult<T>();
    }
}

/// <inheritdoc />
/// <summary>
///     Represents the result of an operation that successfully, or intended to, return a value.
/// </summary>
/// <typeparam name="T">
///     The type of the value.
/// </typeparam>
public sealed class Result<T> : Result
    where T : notnull
{
    internal Result(T value)
    {
        this.Value = value;
    }

    internal Result(FailureReason failureReason)
    {
        this.FailureReason = failureReason;
    }

    /// <inheritdoc />
    public override FailureReason? FailureReason { get; }

    /// <summary>
    ///     Gets the value that the operation successfully, or intended to, return.
    /// </summary>
    /// <remarks>
    ///     This will only have a value if <see cref="IsSuccessful" /> is <c>true</c>.
    /// </remarks>
    public T? Value { get; }

    /// <inheritdoc />
    /// <remarks>
    ///     If this is <c>true</c>, <see cref="FailureReason" /> will have a value.
    ///     If this is <c>false</c>, <see cref="Value" /> will have a value.
    /// </remarks>
    [MemberNotNullWhen(true, nameof(FailureReason))]
    [MemberNotNullWhen(false, nameof(Value))]
    public override bool IsFailed => this.FailureReason != null;

    /// <inheritdoc />
    /// <remarks>
    ///     If this is <c>true</c>, <see cref="Value" /> will have a value.
    ///     If this is <c>false</c>, <see cref="FailureReason" /> will have a value.
    /// </remarks>
    [MemberNotNullWhen(true, nameof(Value))]
    [MemberNotNullWhen(false, nameof(FailureReason))]
    public override bool IsSuccessful => this.Value is not null && !this.IsFailed;
}
