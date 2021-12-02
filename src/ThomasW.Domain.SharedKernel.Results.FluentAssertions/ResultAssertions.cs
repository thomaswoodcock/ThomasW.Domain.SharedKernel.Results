using System.Linq.Expressions;

using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;

namespace ThomasW.Domain.SharedKernel.Results.FluentAssertions;

/// <inheritdoc />
/// <summary>
///     Contains a number of methods used to assert against <see cref="Result" /> objects.
/// </summary>
public class ResultAssertions : ReferenceTypeAssertions<Result, ResultAssertions>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="ResultAssertions" /> class.
    /// </summary>
    /// <param name="result">
    ///     The <see cref="Result" /> object to assert against.
    /// </param>
    public ResultAssertions(Result result)
        : base(result)
    {
    }

    /// <inheritdoc />
    protected override string Identifier => "result";

    /// <summary>
    ///     Asserts that the <see cref="Result" /> is successful.
    /// </summary>
    /// <param name="because">
    ///     A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    ///     is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    ///     Zero or more objects to format using the placeholders in <paramref name="because" />.
    /// </param>
    /// <returns>
    ///     An <see cref="AndConstraint{T}" /> object.
    /// </returns>
    public AndConstraint<ResultAssertions> BeSuccessful(
        string because = "", params object[] becauseArgs)
    {
        _ = Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .ForCondition(this.Subject.IsSuccessful &&
                          !this.Subject.IsFailed &&
                          this.Subject.FailureReason == null)
            .FailWith("Expected a successful result{reason}, but found a failed result with the following " +
                      "failure reason: {0}.",
                this.Subject.FailureReason);

        return new AndConstraint<ResultAssertions>(this);
    }

    /// <summary>
    ///     Asserts that the <see cref="Result" /> is failed.
    /// </summary>
    /// <param name="because">
    ///     A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    ///     is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    ///     Zero or more objects to format using the placeholders in <paramref name="because" />.
    /// </param>
    /// <returns>
    ///     An <see cref="AndConstraint{T}" /> object.
    /// </returns>
    public AndConstraint<ResultAssertions> BeFailed(
        string because = "", params object[] becauseArgs)
    {
        _ = Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .ForCondition(this.Subject.IsFailed &&
                          !this.Subject.IsSuccessful &&
                          this.Subject.FailureReason != null)
            .FailWith("Expected a failed result{reason}, but found a successful result.");

        return new AndConstraint<ResultAssertions>(this);
    }

    /// <inheritdoc cref="ResultAssertions.BeFailed" />
    /// <summary>
    ///     Asserts that the <see cref="Result" /> is failed and has a failure reason of a given type.
    /// </summary>
    /// <typeparam name="T">
    ///     The expected type of the failure reason.
    /// </typeparam>
    public AndConstraint<ResultAssertions> BeFailed<T>(
        string because = "", params object[] becauseArgs)
        where T : FailureReason
    {
        using (new AssertionScope())
        {
            _ = this.Subject.Should().BeFailed(because, becauseArgs);
        }

        _ = Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .ForCondition(this.Subject.FailureReason is T)
            .FailWith("Expected a failure reason of type {0}{reason}, but found {1}.",
                typeof(T),
                this.Subject.FailureReason);

        return new AndConstraint<ResultAssertions>(this);
    }

    /// <inheritdoc cref="ResultAssertions.BeFailed" />
    /// <summary>
    ///     Asserts that the <see cref="Result" /> is failed and has a failure reason, of a given type, that matches a
    ///     predicate.
    /// </summary>
    /// <param name="predicate">
    ///     The predicate with which the failure reason will be matched.
    /// </param>
    /// <param name="because">
    ///     A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    ///     is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    ///     Zero or more objects to format using the placeholders in <paramref name="because" />.
    /// </param>
    /// <typeparam name="T">
    ///     The expected type of the failure reason.
    /// </typeparam>
    public AndConstraint<ResultAssertions> BeFailed<T>(
        Expression<Func<T, bool>> predicate,
        string because = "",
        params object[] becauseArgs)
        where T : FailureReason
    {
        using (new AssertionScope())
        {
            _ = this.Subject.Should().BeFailed<T>(because, becauseArgs);
        }

        _ = Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .ForCondition(this.Subject.FailureReason is T failureReason &&
                          predicate?.Compile()(failureReason) == true)
            .FailWith("Expected failure reason to match {0}{reason}, but found {1}.",
                predicate,
                this.Subject.FailureReason);

        return new AndConstraint<ResultAssertions>(this);
    }
}

/// <inheritdoc />
/// <summary>
///     Contains a number of methods used to assert against <see cref="Result{T}" /> objects.
/// </summary>
/// <typeparam name="T">
///     The type of the value that the result may or may not contain.
/// </typeparam>
public class ResultAssertions<T> : ReferenceTypeAssertions<Result<T>, ResultAssertions<T>>
    where T : notnull
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="ResultAssertions{T}" /> class.
    /// </summary>
    /// <param name="result">
    ///     The <see cref="Result{T}" /> object to assert against.
    /// </param>
    public ResultAssertions(Result<T> result)
        : base(result)
    {
    }

    /// <inheritdoc />
    protected override string Identifier => "result";

    /// <summary>
    ///     Asserts that the <see cref="Result{T}" /> is successful.
    /// </summary>
    /// <param name="because">
    ///     A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    ///     is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    ///     Zero or more objects to format using the placeholders in <paramref name="because" />.
    /// </param>
    /// <returns>
    ///     An <see cref="AndConstraint{T}" /> object.
    /// </returns>
    public AndConstraint<ResultAssertions<T>> BeSuccessful(
        string because = "", params object[] becauseArgs)
    {
        using (new AssertionScope())
        {
            _ = ((Result)this.Subject).Should().BeSuccessful(because, becauseArgs);
        }

        return new AndConstraint<ResultAssertions<T>>(this);
    }

    /// <summary>
    ///     Asserts that the <see cref="Result{T}" /> is successful and its value matches a given <paramref name="predicate" />.
    /// </summary>
    /// <param name="predicate">
    ///     The predicate with which the result value will be matched.
    /// </param>
    /// <param name="because">
    ///     A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    ///     is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    ///     Zero or more objects to format using the placeholders in <paramref name="because" />.
    /// </param>
    /// <returns>
    ///     An <see cref="AndConstraint{T}" /> object.
    /// </returns>
    public AndConstraint<ResultAssertions<T>> BeSuccessful(
        Expression<Func<T, bool>> predicate,
        string because = "",
        params object[] becauseArgs)
    {
        using (new AssertionScope())
        {
            _ = this.Subject.Should().BeSuccessful(because, becauseArgs);
        }

        _ = Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .ForCondition(this.Subject.Value != null && predicate?.Compile()(this.Subject.Value) == true)
            .FailWith("Expected {context:result} value to match {0}{reason}, but found {1}.",
                predicate,
                this.Subject.Value);

        return new AndConstraint<ResultAssertions<T>>(this);
    }

    /// <summary>
    ///     Asserts that the <see cref="Result{T}" /> is failed.
    /// </summary>
    /// <param name="because">
    ///     A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    ///     is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    ///     Zero or more objects to format using the placeholders in <paramref name="because" />.
    /// </param>
    /// <returns>
    ///     An <see cref="AndConstraint{T}" /> object.
    /// </returns>
    public AndConstraint<ResultAssertions<T>> BeFailed(
        string because = "",
        params object[] becauseArgs)
    {
        using (new AssertionScope())
        {
            _ = ((Result)this.Subject).Should().BeFailed(because, becauseArgs);
        }

        return new AndConstraint<ResultAssertions<T>>(this);
    }

    /// <inheritdoc cref="ResultAssertions{T}.BeFailed" />
    /// <summary>
    ///     Asserts that the <see cref="Result{T}" /> is failed and has a failure reason of a given type.
    /// </summary>
    /// <typeparam name="TReason">
    ///     The expected type of the failure reason.
    /// </typeparam>
    public AndConstraint<ResultAssertions<T>> BeFailed<TReason>(
        string because = "",
        params object[] becauseArgs)
        where TReason : FailureReason
    {
        using (new AssertionScope())
        {
            _ = ((Result)this.Subject).Should().BeFailed<TReason>(because, becauseArgs);
        }

        return new AndConstraint<ResultAssertions<T>>(this);
    }

    /// <inheritdoc cref="ResultAssertions{T}.BeFailed" />
    /// <summary>
    ///     Asserts that the <see cref="Result{T}" /> is failed and has a failure reason, of a given type, that matches a
    ///     predicate.
    /// </summary>
    /// <param name="predicate">
    ///     The predicate with which the failure reason will be matched.
    /// </param>
    /// <param name="because">
    ///     A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    ///     is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    ///     Zero or more objects to format using the placeholders in <paramref name="because" />.
    /// </param>
    /// <typeparam name="TReason">
    ///     The expected type of the failure reason.
    /// </typeparam>
    public AndConstraint<ResultAssertions<T>> BeFailed<TReason>(
        Expression<Func<TReason, bool>> predicate,
        string because = "",
        params object[] becauseArgs)
        where TReason : FailureReason
    {
        using (new AssertionScope())
        {
            _ = ((Result)this.Subject).Should().BeFailed(predicate, because, becauseArgs);
        }

        return new AndConstraint<ResultAssertions<T>>(this);
    }
}
