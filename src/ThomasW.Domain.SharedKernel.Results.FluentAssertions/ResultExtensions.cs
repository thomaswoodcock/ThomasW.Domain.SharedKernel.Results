namespace ThomasW.Domain.SharedKernel.Results.FluentAssertions;

/// <summary>
///     Provides extension methods for asserting against <see cref="Result" /> and <see cref="Result{T}" /> objects.
/// </summary>
public static class ResultExtensions
{
    /// <summary>
    ///     Returns a <see cref="ResultAssertions" /> object that can be used to assert against the current
    ///     <paramref name="result" />.
    /// </summary>
    /// <param name="result">
    ///     The <see cref="Result" /> object to assert against.
    /// </param>
    /// <returns>
    ///     A <see cref="ResultAssertions" /> object.
    /// </returns>
    public static ResultAssertions Should(this Result result)
    {
        return new ResultAssertions(result);
    }

    /// <summary>
    ///     Returns a <see cref="ResultAssertions{T}" /> object that can be used to assert against the current
    ///     <paramref name="result" />.
    /// </summary>
    /// <param name="result">
    ///     The <see cref="Result{T}" /> object to assert against.
    /// </param>
    /// <typeparam name="T">
    ///     The type of the value that the result may or may not contain.
    /// </typeparam>
    /// <returns>
    ///     A <see cref="ResultAssertions{T}" /> object.
    /// </returns>
    public static ResultAssertions<T> Should<T>(this Result<T> result)
        where T : notnull
    {
        return new ResultAssertions<T>(result);
    }
}
