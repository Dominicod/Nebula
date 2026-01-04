using Nebula.Services.Contracts;

namespace Nebula.Services.Common;

/// <summary>
/// Represents the result of an operation with typed data and error handling.
/// </summary>
/// <typeparam name="T">The type of data being returned, must implement IResponse.</typeparam>
public sealed record TypedResult<T> where T : IResponse
{
    /// <summary>
    /// Gets a value indicating whether the operation was successful.
    /// </summary>
    public bool IsSuccess { get; init; }

    /// <summary>
    /// Gets the data returned by the operation (null if failed).
    /// </summary>
    public T? Data { get; init; }

    /// <summary>
    /// Gets the error message if the operation failed.
    /// </summary>
    public string? ErrorMessage { get; init; }

    /// <summary>
    /// Gets the error code if the operation failed.
    /// </summary>
    public string? ErrorCode { get; init; }

    /// <summary>
    /// Gets the exception details if an exception occurred.
    /// </summary>
    public string? ExceptionDetails { get; init; }

    /// <summary>
    /// Gets additional error metadata.
    /// </summary>
    public Dictionary<string, object>? ErrorMetadata { get; init; }

    private TypedResult() { }

    /// <summary>
    /// Creates a successful result with data.
    /// </summary>
    /// <param name="data">The successful operation data.</param>
    /// <returns>A successful TypedResult.</returns>
    public static TypedResult<T> Success(T data)
    {
        return new TypedResult<T>
        {
            IsSuccess = true,
            Data = data,
            ErrorMessage = null,
            ErrorCode = null,
            ExceptionDetails = null,
            ErrorMetadata = null
        };
    }

    /// <summary>
    /// Creates a failed result with an error message.
    /// </summary>
    /// <param name="errorMessage">The error message.</param>
    /// <param name="errorCode">Optional error code.</param>
    /// <returns>A failed TypedResult.</returns>
    public static TypedResult<T> Failure(string errorMessage, string? errorCode = null)
    {
        return new TypedResult<T>
        {
            IsSuccess = false,
            Data = default,
            ErrorMessage = errorMessage,
            ErrorCode = errorCode,
            ExceptionDetails = null,
            ErrorMetadata = null
        };
    }

    /// <summary>
    /// Creates a failed result from an exception.
    /// </summary>
    /// <param name="exception">The exception that occurred.</param>
    /// <param name="errorMessage">Optional custom error message.</param>
    /// <param name="errorCode">Optional error code.</param>
    /// <returns>A failed TypedResult.</returns>
    public static TypedResult<T> Exception(Exception exception, string? errorMessage = null, string? errorCode = null)
    {
        return new TypedResult<T>
        {
            IsSuccess = false,
            Data = default,
            ErrorMessage = errorMessage ?? exception.Message,
            ErrorCode = errorCode ?? "EXCEPTION",
            ExceptionDetails = exception.ToString(),
            ErrorMetadata = new Dictionary<string, object>
            {
                { "ExceptionType", exception.GetType().Name }
            }
        };
    }

    /// <summary>
    /// Creates a failed result with detailed error information.
    /// </summary>
    /// <param name="errorMessage">The error message.</param>
    /// <param name="errorCode">The error code.</param>
    /// <param name="metadata">Additional error metadata.</param>
    /// <returns>A failed TypedResult.</returns>
    public static TypedResult<T> FailureDetailed(
        string errorMessage,
        string errorCode,
        Dictionary<string, object>? metadata = null)
    {
        return new TypedResult<T>
        {
            IsSuccess = false,
            Data = default,
            ErrorMessage = errorMessage,
            ErrorCode = errorCode,
            ExceptionDetails = null,
            ErrorMetadata = metadata
        };
    }
}