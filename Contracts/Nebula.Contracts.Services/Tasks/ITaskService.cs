using Nebula.DataTransfer.Common;
using Nebula.DataTransfer.Contracts.Tasks;

namespace Nebula.Contracts.Services.Tasks;

/// <summary>
///     Service interface for Task business operations.
///     Works with API contracts (DTOs) and never exposes domain entities.
/// </summary>
public interface ITaskService
{
    /// <summary>
    ///     Gets a task by its unique identifier.
    /// </summary>
    /// <param name="id">The task's unique identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The task response if found, null otherwise.</returns>
    Task<TaskResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Gets all tasks.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>TypedResult containing the list of all tasks.</returns>
    Task<TypedResult<TaskListResponse>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    ///     Gets tasks for a specific date.
    /// </summary>
    /// <param name="date">The date to filter tasks by (uses CreatedAt).</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>TypedResult containing the list of tasks for the date.</returns>
    Task<TypedResult<TaskListResponse>> GetByDateAsync(DateOnly date, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Creates a new task.
    /// </summary>
    /// <param name="command">The create task command.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>TypedResult containing the created task response.</returns>
    Task<TypedResult<TaskResponse>> CreateAsync(CreateTaskCommand command,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     Deletes a task by its unique identifier.
    /// </summary>
    /// <param name="id">The task's unique identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>TypedResult indicating success or failure.</returns>
    Task<TypedResult<TaskResponse>> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Marks a task as completed.
    /// </summary>
    /// <param name="id">The task's unique identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>TypedResult containing the updated task response.</returns>
    Task<TypedResult<TaskResponse>> MarkAsCompletedAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Marks a task as incomplete.
    /// </summary>
    /// <param name="id">The task's unique identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>TypedResult containing the updated task response.</returns>
    Task<TypedResult<TaskResponse>> MarkAsIncompleteAsync(Guid id, CancellationToken cancellationToken = default);
}
