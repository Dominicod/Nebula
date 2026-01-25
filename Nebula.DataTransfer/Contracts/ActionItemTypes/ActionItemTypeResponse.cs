using Nebula.Contracts.Common;

namespace Nebula.DataTransfer.Contracts.ActionItemTypes;

/// <summary>
///     Response contract representing an action item type.
/// </summary>
public sealed record ActionItemTypeResponse : IResponse
{
    /// <summary>
    ///     Gets the unique identifier of the action item type.
    /// </summary>
    public required Guid Id { get; init; }

    /// <summary>
    ///     Gets the name of the action item type.
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    ///     Gets the date and time when the action item type was created.
    /// </summary>
    public required DateTimeOffset CreatedAt { get; init; }

    /// <summary>
    ///     Gets the date and time when the action item type was last updated.
    /// </summary>
    public required DateTimeOffset UpdatedAt { get; init; }
}
