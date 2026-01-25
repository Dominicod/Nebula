using Nebula.DataTransfer.Common;
using Nebula.DataTransfer.Contracts.ActionItemTypes;

namespace Nebula.Contracts.Services.ActionItemTypes;

/// <summary>
///     Service interface for ActionItemType business operations.
///     Works with API contracts (DTOs) and never exposes domain entities.
/// </summary>
public interface IActionItemTypeService
{
    /// <summary>
    ///     Gets an action item type by its unique identifier.
    /// </summary>
    /// <param name="id">The action item type's unique identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The action item type response if found, null otherwise.</returns>
    Task<ActionItemTypeResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Gets all action item types.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>TypedResult containing the list of all action item types.</returns>
    Task<TypedResult<ActionItemTypeListResponse>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    ///     Creates a new action item type.
    /// </summary>
    /// <param name="command">The create action item type command.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>TypedResult containing the created action item type response.</returns>
    Task<TypedResult<ActionItemTypeResponse>> CreateAsync(CreateActionItemTypeCommand command,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     Deletes an action item type by its unique identifier.
    /// </summary>
    /// <param name="id">The action item type's unique identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>TypedResult indicating success or failure.</returns>
    Task<TypedResult<ActionItemTypeResponse>> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
