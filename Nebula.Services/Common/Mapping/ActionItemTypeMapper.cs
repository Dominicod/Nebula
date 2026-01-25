using Nebula.DataTransfer.Contracts.ActionItemTypes;
using Nebula.Domain.Entities.ActionItemTypes;

namespace Nebula.Services.Common.Mapping;

/// <summary>
///     Mapper for ActionItemType entity to/from DTOs.
/// </summary>
internal static class ActionItemTypeMapper
{
    /// <summary>
    ///     Maps an ActionItemType entity to ActionItemTypeResponse DTO.
    /// </summary>
    /// <param name="actionItemType">The ActionItemType entity.</param>
    /// <returns>ActionItemTypeResponse DTO.</returns>
    public static ActionItemTypeResponse ToResponse(ActionItemType actionItemType)
    {
        return new ActionItemTypeResponse
        {
            Id = actionItemType.Id,
            Name = actionItemType.Name,
            CreatedAt = actionItemType.CreatedAt,
            UpdatedAt = actionItemType.UpdatedAt
        };
    }

    /// <summary>
    ///     Maps a collection of ActionItemType entities to ActionItemTypeListResponse DTO.
    /// </summary>
    /// <param name="actionItemTypes">The collection of ActionItemType entities.</param>
    /// <returns>ActionItemTypeListResponse DTO.</returns>
    public static ActionItemTypeListResponse ToListResponse(IEnumerable<ActionItemType> actionItemTypes)
    {
        var actionItemTypeList = actionItemTypes.ToList();
        return new ActionItemTypeListResponse
        {
            ActionItemTypes = actionItemTypeList.Select(ToResponse),
            TotalCount = actionItemTypeList.Count
        };
    }

    /// <summary>
    ///     Creates an ActionItemType entity from CreateActionItemTypeCommand.
    ///     Note: CreatedAt/UpdatedAt are managed by the database via EF Core configuration.
    /// </summary>
    /// <param name="command">The CreateActionItemTypeCommand.</param>
    /// <returns>A new ActionItemType entity.</returns>
    public static ActionItemType FromCreateCommand(CreateActionItemTypeCommand command)
    {
        return new ActionItemType
        {
            Id = Guid.NewGuid(),
            Name = command.Name
        };
    }
}
