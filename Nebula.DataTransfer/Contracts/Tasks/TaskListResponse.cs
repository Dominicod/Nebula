namespace Nebula.DataTransfer.Contracts.Tasks;

/// <summary>
///     Response contract representing a collection of tasks.
/// </summary>
public sealed record TaskListResponse : IResponse
{
    /// <summary>
    ///     Gets the collection of tasks.
    /// </summary>
    public required IEnumerable<TaskResponse> Tasks { get; init; }

    /// <summary>
    ///     Gets the total count of tasks.
    /// </summary>
    public required int TotalCount { get; init; }
}