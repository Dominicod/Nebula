using Nebula.DataTransfer.Contracts.ActionItems;
using Nebula.Domain.Entities.ActionItems;
using Nebula.Domain.Entities.ActionItemTypes;

namespace Nebula.Services.Common.Mapping;

/// <summary>
///     Mapper for ActionItem entity to/from DTOs.
/// </summary>
internal static class ActionItemMapper
{
    /// <summary>
    ///     Maps a ActionItem entity to ActionItemResponse DTO.
    /// </summary>
    /// <param name="actionItem">The ActionItem entity.</param>
    /// <returns>ActionItemResponse DTO.</returns>
    public static ActionItemResponse ToResponse(ActionItem actionItem)
    {
        return new ActionItemResponse
        {
            Id = actionItem.Id,
            Text = actionItem.Text,
            IsCompleted = actionItem.IsCompleted,
            CompletedAt = actionItem.CompletedAt,
            ActionItemTypeId = actionItem.ActionItemTypeId,
            ActionItemTypeName = actionItem.ActionItemType.Name,
            CreatedAt = actionItem.CreatedAt,
            UpdatedAt = actionItem.UpdatedAt
        };
    }

    /// <summary>
    ///     Maps a collection of ActionItem entities to ActionItemListResponse DTO.
    /// </summary>
    /// <param name="actionItems">The collection of ActionItem entities.</param>
    /// <returns>ActionItemListResponse DTO.</returns>
    public static ActionItemListResponse ToListResponse(IEnumerable<ActionItem> actionItems)
    {
        var actionItemList = actionItems.ToList();
        return new ActionItemListResponse
        {
            Tasks = actionItemList.Select(ToResponse),
            TotalCount = actionItemList.Count
        };
    }

    /// <summary>
    ///     Creates a ActionItem entity from CreateActionItemCommand.
    ///     Note: CreatedAt/UpdatedAt are managed by the database via EF Core configuration.
    /// </summary>
    /// <param name="command">The CreateActionItemCommand.</param>
    /// <param name="actionItemType">The ActionItemType entity.</param>
    /// <returns>A new ActionItem entity.</returns>
    public static ActionItem FromCreateCommand(CreateActionItemCommand command, ActionItemType actionItemType)
    {
        return new ActionItem
        {
            Id = Guid.NewGuid(),
            Text = command.Text,
            IsCompleted = false,
            CompletedAt = null,
            ActionItemTypeId = actionItemType.Id,
            ActionItemType = actionItemType
        };
    }
}