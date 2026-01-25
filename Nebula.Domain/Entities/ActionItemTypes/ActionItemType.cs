using Nebula.Domain.Entities.Common;

namespace Nebula.Domain.Entities.ActionItemTypes;

/// <summary>
///     Represents a type/category for action items.
/// </summary>
public class ActionItemType : BaseEntity<Guid>
{
    /// <summary>
    ///     The name of the action item type.
    /// </summary>
    public required string Name { get; init; }
}
