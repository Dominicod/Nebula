using Nebula.DataTransfer.Contracts.DailyTasks;
using Nebula.Domain.Entities.DailyTasks;

namespace Nebula.Services.Mapping;

/// <summary>
///     Mapper for DailyTask entity to/from DTOs.
/// </summary>
internal static class DailyTaskMapper
{
    /// <summary>
    ///     Maps a DailyTask entity to DailyTaskResponse DTO.
    /// </summary>
    /// <param name="task">The DailyTask entity.</param>
    /// <returns>DailyTaskResponse DTO.</returns>
    public static DailyTaskResponse ToResponse(DailyTask task)
    {
        return new DailyTaskResponse
        {
            Id = task.Id,
            Text = task.Text,
            IsCompleted = task.IsCompleted,
            CompletedAt = task.CompletedAt,
            CreatedAt = task.CreatedAt,
            UpdatedAt = task.UpdatedAt
        };
    }

    /// <summary>
    ///     Maps a collection of DailyTask entities to DailyTaskListResponse DTO.
    /// </summary>
    /// <param name="tasks">The collection of DailyTask entities.</param>
    /// <returns>DailyTaskListResponse DTO.</returns>
    public static DailyTaskListResponse ToListResponse(IEnumerable<DailyTask> tasks)
    {
        var taskList = tasks.ToList();
        return new DailyTaskListResponse
        {
            DailyTasks = taskList.Select(ToResponse),
            TotalCount = taskList.Count
        };
    }

    /// <summary>
    ///     Creates a DailyTask entity from CreateDailyTaskCommand.
    ///     Note: CreatedAt/UpdatedAt are managed by the database via EF Core configuration.
    /// </summary>
    /// <param name="command">The CreateDailyTaskCommand.</param>
    /// <returns>A new DailyTask entity.</returns>
    public static DailyTask FromCreateCommand(CreateDailyTaskCommand command)
    {
        return new DailyTask
        {
            Id = Guid.NewGuid(),
            Text = command.Text,
            IsCompleted = false,
            CompletedAt = null
        };
    }

    /// <summary>
    ///     Creates a new DailyTask entity from UpdateDailyTaskCommand with the existing ID.
    ///     Note: Since DailyTask uses init-only properties for Text, we create a new instance.
    ///     EF Core will track and update the existing entity.
    /// </summary>
    /// <param name="existingId">The existing DailyTask entity ID.</param>
    /// <param name="command">The UpdateDailyTaskCommand.</param>
    /// <param name="existingTask">The existing task to preserve IsCompleted and CompletedAt.</param>
    /// <returns>A new DailyTask entity with updated values.</returns>
    public static DailyTask FromUpdateCommand(Guid existingId, UpdateDailyTaskCommand command, DailyTask existingTask)
    {
        return new DailyTask
        {
            Id = existingId,
            Text = command.Text,
            IsCompleted = existingTask.IsCompleted,
            CompletedAt = existingTask.CompletedAt
        };
    }
}
