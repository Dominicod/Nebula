namespace Nebula.Domain.Entities.Shared;

/// <summary>
/// Base entity providing common properties for all domain entities.
/// </summary>
/// <typeparam name="TId">The type of the entity identifier (e.g., Guid, int, string).</typeparam>
public abstract class BaseEntity<TId>
{
    /// <summary>
    /// The unique identifier for the entity.
    /// </summary>
    public TId Id { get; init; } = default!;

    /// <summary>
    /// The date and time when the entity was created.
    /// </summary>
    public DateTime CreatedAt { get; private init; }

    /// <summary>
    /// The date and time when the entity was last updated.
    /// </summary>
    public DateTime UpdatedAt { get; private set; }
}