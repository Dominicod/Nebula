using Nebula.DataTransfer.Common;
using Nebula.DataTransfer.Contracts.ActionItems;

namespace Nebula.Contracts.Services.ActionItems;

/// <summary>
///     Service interface for Task business operations.
///     Works with API contracts (DTOs) and never exposes domain entities.
/// </summary>
public interface IActionItemService
{
    /// <summary>
    ///     Gets a actionItem by its unique identifier.
    /// </summary>
    /// <param name="id">The actionItem's unique identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The actionItem response if found, null otherwise.</returns>
    Task<ActionItemResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Gets all actionItems.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>TypedResult containing the list of all actionItems.</returns>
    Task<TypedResult<ActionItemListResponse>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    ///     Gets actionItems for a specific date.
    /// </summary>
    /// <param name="date">The date to filter actionItems by (uses CreatedAt).</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>TypedResult containing the list of actionItems for the date.</returns>
    Task<TypedResult<ActionItemListResponse>> GetByDateAsync(DateOnly date, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Creates a new actionItem.
    /// </summary>
    /// <param name="command">The create actionItem command.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>TypedResult containing the created actionItem response.</returns>
    Task<TypedResult<ActionItemResponse>> CreateAsync(CreateActionItemCommand command,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     Deletes a actionItem by its unique identifier.
    /// </summary>
    /// <param name="id">The actionItem's unique identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>TypedResult indicating success or failure.</returns>
    Task<TypedResult<ActionItemResponse>> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Marks a actionItem as completed.
    /// </summary>
    /// <param name="id">The actionItem's unique identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>TypedResult containing the updated actionItem response.</returns>
    Task<TypedResult<ActionItemResponse>> MarkAsCompletedAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Marks a actionItem as incomplete.
    /// </summary>
    /// <param name="id">The actionItem's unique identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>TypedResult containing the updated actionItem response.</returns>
    Task<TypedResult<ActionItemResponse>> MarkAsIncompleteAsync(Guid id, CancellationToken cancellationToken = default);
}