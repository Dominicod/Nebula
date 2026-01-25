using Nebula.Contracts.Common;

namespace Nebula.DataTransfer.Contracts.ActionItems;

/// <summary>
///     Response contract representing a actionItem.
/// </summary>
public sealed record ActionItemResponse : IResponse
{
    /// <summary>
    ///     Gets the unique identifier of the actionItem.
    /// </summary>
    public required Guid Id { get; init; }

    /// <summary>
    ///     Gets the text content of the actionItem.
    /// </summary>
    public required string Text { get; init; }

    /// <summary>
    ///     Gets whether the actionItem has been completed.
    /// </summary>
    public required bool IsCompleted { get; init; }

    /// <summary>
    ///     Gets when the actionItem was marked as completed (null if not completed).
    /// </summary>
    public DateTime? CompletedAt { get; init; }

    /// <summary>
    ///     Gets the action item type identifier.
    /// </summary>
    public required Guid ActionItemTypeId { get; init; }

    /// <summary>
    ///     Gets the name of the action item type.
    /// </summary>
    public required string ActionItemTypeName { get; init; }

    /// <summary>
    ///     Gets the date and time when the actionItem was created.
    /// </summary>
    public required DateTimeOffset CreatedAt { get; init; }

    /// <summary>
    ///     Gets the date and time when the actionItem was last updated.
    /// </summary>
    public required DateTimeOffset UpdatedAt { get; init; }
}