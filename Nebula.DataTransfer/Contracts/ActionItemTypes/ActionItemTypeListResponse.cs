using Nebula.Contracts.Common;

namespace Nebula.DataTransfer.Contracts.ActionItemTypes;

/// <summary>
///     Response contract representing a collection of action item types.
/// </summary>
public sealed record ActionItemTypeListResponse : IResponse
{
    /// <summary>
    ///     Gets the collection of action item types.
    /// </summary>
    public required IEnumerable<ActionItemTypeResponse> ActionItemTypes { get; init; }

    /// <summary>
    ///     Gets the total count of action item types.
    /// </summary>
    public required int TotalCount { get; init; }
}
