namespace Nebula.Services.DTOs.Networking;

/// <summary>
/// Data transfer object for Person entity.
/// Used by repositories to return data without exposing domain entities.
/// </summary>
public sealed record PersonDto
{
    /// <summary>
    /// Gets the unique identifier of the person.
    /// </summary>
    public required Guid Id { get; init; }

    /// <summary>
    /// Gets the first name of the person.
    /// </summary>
    public required string FirstName { get; init; }

    /// <summary>
    /// Gets the last name of the person.
    /// </summary>
    public required string LastName { get; init; }

    /// <summary>
    /// Gets the date and time when the person was created.
    /// </summary>
    public required DateTimeOffset CreatedAt { get; init; }

    /// <summary>
    /// Gets the date and time when the person was last updated.
    /// </summary>
    public required DateTimeOffset UpdatedAt { get; init; }
}
