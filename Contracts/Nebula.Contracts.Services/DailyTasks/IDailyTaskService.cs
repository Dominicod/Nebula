using Nebula.DataTransfer.Common;
using Nebula.DataTransfer.Contracts.DailyTasks;

namespace Nebula.Contracts.Services.DailyTasks;

/// <summary>
///     Service interface for DailyTask business operations.
///     Works with API contracts (DTOs) and never exposes domain entities.
/// </summary>
public interface IDailyTaskService
{
    /// <summary>
    ///     Gets a daily task by its unique identifier.
    /// </summary>
    /// <param name="id">The task's unique identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>TypedResult containing the task response if found.</returns>
    Task<TypedResult<DailyTaskResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Gets all daily tasks.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>TypedResult containing the list of all tasks.</returns>
    Task<TypedResult<DailyTaskListResponse>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    ///     Gets daily tasks for a specific date.
    /// </summary>
    /// <param name="date">The date to filter tasks by (uses CreatedAt).</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>TypedResult containing the list of tasks for the date.</returns>
    Task<TypedResult<DailyTaskListResponse>> GetByDateAsync(DateOnly date, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Creates a new daily task.
    /// </summary>
    /// <param name="command">The create task command.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>TypedResult containing the created task response.</returns>
    Task<TypedResult<DailyTaskResponse>> CreateAsync(CreateDailyTaskCommand command,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     Updates an existing daily task.
    /// </summary>
    /// <param name="id">The task's unique identifier.</param>
    /// <param name="command">The update task command.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>TypedResult containing the updated task response if found.</returns>
    Task<TypedResult<DailyTaskResponse>> UpdateAsync(Guid id, UpdateDailyTaskCommand command,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     Deletes a daily task by its unique identifier.
    /// </summary>
    /// <param name="id">The task's unique identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>TypedResult indicating success or failure.</returns>
    Task<TypedResult<DailyTaskResponse>> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Marks a daily task as completed.
    /// </summary>
    /// <param name="id">The task's unique identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>TypedResult containing the updated task response.</returns>
    Task<TypedResult<DailyTaskResponse>> MarkAsCompletedAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Marks a daily task as incomplete.
    /// </summary>
    /// <param name="id">The task's unique identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>TypedResult containing the updated task response.</returns>
    Task<TypedResult<DailyTaskResponse>> MarkAsIncompleteAsync(Guid id, CancellationToken cancellationToken = default);
}
