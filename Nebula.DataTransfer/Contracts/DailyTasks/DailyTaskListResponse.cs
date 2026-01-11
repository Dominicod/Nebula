namespace Nebula.DataTransfer.Contracts.DailyTasks;

/// <summary>
///     Response contract representing a collection of daily tasks.
/// </summary>
public sealed record DailyTaskListResponse : IResponse
{
    /// <summary>
    ///     Gets the collection of daily tasks.
    /// </summary>
    public required IEnumerable<DailyTaskResponse> DailyTasks { get; init; }

    /// <summary>
    ///     Gets the total count of daily tasks.
    /// </summary>
    public required int TotalCount { get; init; }
}
