using Nebula.Domain.Entities.Shared;

namespace Nebula.Domain.Entities.Networking;

/// <summary>
/// Represents a person in the networking contacts system.
/// </summary>
public class Person : BaseEntity<Guid>
{
    /// <summary>
    /// Represents the first name of the Person
    /// </summary>
    public required string FirstName { get; set; }

    /// <summary>
    /// Represents the last name of the Person
    /// </summary>
    public required string LastName { get; set; }
}