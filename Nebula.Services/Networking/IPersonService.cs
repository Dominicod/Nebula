using Nebula.Services.Common;
using Nebula.Services.Contracts.Networking;

namespace Nebula.Services.Networking;

/// <summary>
/// Service interface for Person business operations.
/// Works with API contracts (DTOs) and never exposes domain entities.
/// </summary>
public interface IPersonService
{
    /// <summary>
    /// Gets all persons.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>TypedResult containing collection of person responses.</returns>
    Task<TypedResult<PersonListResponse>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a person by their unique identifier.
    /// </summary>
    /// <param name="id">The person's unique identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>TypedResult containing person response if found.</returns>
    Task<TypedResult<PersonResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

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
