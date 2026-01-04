namespace Nebula.Services.Contracts.Networking;

/// <summary>
/// Response contract representing a collection of persons.
/// </summary>
public sealed record PersonListResponse : IResponse
{
    /// <summary>
    /// Gets the collection of persons.
    /// </summary>
    public required IEnumerable<PersonResponse> Persons { get; init; }

    /// <summary>
    /// Gets the total count of persons.
    /// </summary>
    public required int TotalCount { get; init; }
}
