using Nebula.DataTransfer.Common;
using Nebula.DataTransfer.Contracts.Networking;

namespace Nebula.Contracts.Services.Networking;

/// <summary>
/// Service interface for Person business operations.
/// Works with API contracts (DTOs) and never exposes domain entities.
/// </summary>
public interface IPersonService
{
    /// <summary>
    /// Creates a new person.
    /// </summary>
    /// <param name="request">The create person request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>TypedResult containing the created person response.</returns>
    Task<TypedResult<PersonResponse>> CreateAsync(CreatePersonRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing person.
    /// </summary>
    /// <param name="id">The person's unique identifier.</param>
    /// <param name="request">The update person request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>TypedResult containing the updated person response if found.</returns>
    Task<TypedResult<PersonResponse>> UpdateAsync(Guid id, UpdatePersonRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a person by their unique identifier.
    /// </summary>
    /// <param name="id">The person's unique identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>TypedResult indicating success or failure.</returns>
    Task<TypedResult<PersonResponse>> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}