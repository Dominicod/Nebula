namespace Nebula.DataTransfer.Contracts.Tasks;

/// <summary>
///     Response contract representing a task.
/// </summary>
public sealed record TaskResponse : IResponse
{
    /// <summary>
    ///     Gets the unique identifier of the task.
    /// </summary>
    public required Guid Id { get; init; }

    /// <summary>
    ///     Gets the text content of the task.
    /// </summary>
    public required string Text { get; init; }

    /// <summary>
    ///     Gets whether the task has been completed.
    /// </summary>
    public required bool IsCompleted { get; init; }

    /// <summary>
    ///     Gets when the task was marked as completed (null if not completed).
    /// </summary>
    public DateTime? CompletedAt { get; init; }

    /// <summary>
    ///     Gets the date and time when the task was created.
    /// </summary>
    public required DateTimeOffset CreatedAt { get; init; }

    /// <summary>
    ///     Gets the date and time when the task was last updated.
    /// </summary>
    public required DateTimeOffset UpdatedAt { get; init; }
}