using Nebula.Domain.Entities.ActionItemTypes;
using Nebula.Domain.Entities.Common;

namespace Nebula.Domain.Entities.ActionItems;

/// <summary>
///     Represents a actionItem in the actionItem tracking system.
/// </summary>
public class ActionItem : BaseEntity<Guid>
{
    /// <summary>
    ///     The text content of the actionItem.
    /// </summary>
    public required string Text { get; init; }

    /// <summary>
    ///     Whether the actionItem has been completed.
    /// </summary>
    public bool IsCompleted { get; set; }

    /// <summary>
    ///     When the actionItem was marked as completed (null if not completed).
    /// </summary>
    public DateTime? CompletedAt { get; set; }

    /// <summary>
    ///     The foreign key for the action item type.
    /// </summary>
    public Guid? ActionItemTypeId { get; set; }

    /// <summary>
    ///     The type/category of this action item.
    /// </summary>
    public ActionItemType? ActionItemType { get; set; }
}