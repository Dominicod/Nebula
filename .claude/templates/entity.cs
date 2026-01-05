// Template: Domain Entity
// Location: Nebula.Domain/Entities/{Domain}/{EntityName}.cs
// Example: Nebula.Domain/Entities/Networking/Person.cs

using Nebula.Domain.Common;

namespace Nebula.Domain.Entities.{Domain};

/// <summary>
///     Represents a {EntityName} entity.
/// </summary>
public class {EntityName} : BaseEntity<Guid>
{
    /// <summary>
    ///     Gets or initializes the {property description}.
    /// </summary>
    public required string PropertyName { get; init; }

    /// <summary>
    ///     Gets or initializes another property.
    /// </summary>
    public string? OptionalProperty { get; init; }

    // Navigation properties (if needed)
    // public ICollection<RelatedEntity> RelatedEntities { get; init; } = [];
}

// Notes:
// - Extend BaseEntity<TId> (provides Id, CreatedAt, UpdatedAt)
// - Use 'required' for mandatory properties
// - Use 'init' for immutability
// - Don't add business logic - entities are anemic
// - Add XML comments for all public members
