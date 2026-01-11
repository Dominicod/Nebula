using Nebula.DataTransfer.Common;
using Nebula.DataTransfer.Contracts.Networking;

namespace Nebula.Contracts.Services.Networking;

/// <summary>
///     Service interface for Person business operations.
///     Works with API contracts (DTOs) and never exposes domain entities.
/// </summary>
public interface IPersonService
{
    /// <summary>
    ///     Gets a person by their unique identifier.
    /// </summary>
    /// <param name="id">The person's unique identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The person response if found, null otherwise.</returns>
    Task<PersonResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Gets all persons.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>TypedResult containing the list of all persons.</returns>
    Task<TypedResult<PersonListResponse>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    ///     Creates a new person.
    /// </summary>
    /// <param name="command">The create person command.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>TypedResult containing the created person response.</returns>
    Task<TypedResult<PersonResponse>> CreateAsync(CreatePersonCommand command,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     Deletes a person by their unique identifier.
    /// </summary>
    /// <param name="id">The person's unique identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>TypedResult indicating success or failure.</returns>
    Task<TypedResult<PersonResponse>> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
