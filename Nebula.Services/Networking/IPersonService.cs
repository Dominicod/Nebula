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
    /// <returns>Collection of person responses.</returns>
    Task<IEnumerable<PersonResponse>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a person by their unique identifier.
    /// </summary>
    /// <param name="id">The person's unique identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Person response if found, null otherwise.</returns>
    Task<PersonResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new person.
    /// </summary>
    /// <param name="request">The create person request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The created person response.</returns>
    Task<PersonResponse> CreateAsync(CreatePersonRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing person.
    /// </summary>
    /// <param name="id">The person's unique identifier.</param>
    /// <param name="request">The update person request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The updated person response if found, null otherwise.</returns>
    Task<PersonResponse?> UpdateAsync(Guid id, UpdatePersonRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a person by their unique identifier.
    /// </summary>
    /// <param name="id">The person's unique identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if the person was deleted, false if not found.</returns>
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
