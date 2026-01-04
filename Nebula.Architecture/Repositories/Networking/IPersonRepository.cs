using Nebula.Architecture.DTOs.Networking;

namespace Nebula.Architecture.Repositories.Networking;

/// <summary>
/// Repository interface for Person operations.
/// Returns DTOs to prevent domain entity exposure.
/// </summary>
public interface IPersonRepository
{
    /// <summary>
    /// Gets all persons.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Collection of person DTOs.</returns>
    Task<IEnumerable<PersonDto>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a person by their unique identifier.
    /// </summary>
    /// <param name="id">The person's unique identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Person DTO if found, null otherwise.</returns>
    Task<PersonDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new person.
    /// </summary>
    /// <param name="firstName">The person's first name.</param>
    /// <param name="lastName">The person's last name.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The created person DTO.</returns>
    Task<PersonDto> CreateAsync(string firstName, string lastName, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing person.
    /// </summary>
    /// <param name="id">The person's unique identifier.</param>
    /// <param name="firstName">The person's first name.</param>
    /// <param name="lastName">The person's last name.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The updated person DTO if found, null otherwise.</returns>
    Task<PersonDto?> UpdateAsync(Guid id, string firstName, string lastName, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a person by their unique identifier.
    /// </summary>
    /// <param name="id">The person's unique identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if the person was deleted, false if not found.</returns>
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
