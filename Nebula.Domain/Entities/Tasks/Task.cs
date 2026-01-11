using Nebula.Domain.Entities.Shared;

namespace Nebula.Domain.Entities.Tasks;

/// <summary>
///     Represents a task in the task tracking system.
/// </summary>
public class Task : BaseEntity<Guid>
{
    /// <summary>
    ///     The text content of the task.
    /// </summary>
    public required string Text { get; init; }

    /// <summary>
    ///     Whether the task has been completed.
    /// </summary>
    public bool IsCompleted { get; set; }

    /// <summary>
    ///     When the task was marked as completed (null if not completed).
    /// </summary>
    public DateTime? CompletedAt { get; set; }
}
