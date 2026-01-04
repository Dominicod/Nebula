using Nebula.DataTransfer.Contracts;

namespace Nebula.DataTransfer.Common;

/// <summary>
/// Represents the result of an operation with typed data and error handling.
/// </summary>
/// <typeparam name="T">The type of data being returned, must implement IResponse.</typeparam>
public sealed record TypedResult<T> where T : IResponse
{
    /// <summary>
    /// Gets a value indicating whether the operation was successful.
    /// Returns true when there are no error messages and no exception has occurred.
    /// </summary>
    public bool IsSuccess => ErrorMessages.Count == 0 || HasException;

    /// <summary>
    /// Gets a value indicating whether an exception has occurred during the operation.
    /// </summary>
    public bool HasException => Exception != null;

    /// <summary>
    /// Gets the data returned by the operation.
    /// </summary>
    public T? Data { get; private init; }

    /// <summary>
    /// Gets the collection of error messages that occurred during the operation.
    /// </summary>
    public List<string> ErrorMessages { get; } = [];

    /// <summary>
    /// Gets the exception that occurred during the operation, if any.
    /// </summary>
    public Exception? Exception { get; private set; }

    private TypedResult() { }

    /// <summary>
    /// Creates a new result with the specified data.
    /// </summary>
    /// <param name="data">The data to include in the result.</param>
    /// <returns>A new TypedResult instance.</returns>
    public static TypedResult<T> Result(T? data = default)
    {
        return new TypedResult<T>
        {
            Data = data
        };
    }

    /// <summary>
    /// Adds an error message to the result.
    /// </summary>
    /// <param name="errorMessage">The error message to add.</param>
    /// <returns>The current TypedResult instance for method chaining.</returns>
    public TypedResult<T> WithErrorMessage(string errorMessage)
    {
        ErrorMessages.Add(errorMessage);
        return this;
    }

    /// <summary>
    /// Sets an exception on the result.
    /// </summary>
    /// <param name="exception">The exception that occurred.</param>
    /// <returns>The current TypedResult instance for method chaining.</returns>
    public TypedResult<T> WithException(Exception exception)
    {
        Exception = exception;
        return this;
    }
}