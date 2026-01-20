using Nebula.Contracts.Common;

namespace Nebula.DataTransfer.Contracts.ActionItems;

/// <summary>
///     Response contract representing a collection of actionItems.
/// </summary>
public sealed record ActionItemListResponse : IResponse
{
    /// <summary>
    ///     Gets the collection of actionItems.
    /// </summary>
    public required IEnumerable<ActionItemResponse> Tasks { get; init; }

    /// <summary>
    ///     Gets the total count of actionItems.
    /// </summary>
    public required int TotalCount { get; init; }
}