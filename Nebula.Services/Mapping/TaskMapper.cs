using Nebula.DataTransfer.Contracts.Tasks;

namespace Nebula.Services.Mapping;

/// <summary>
///     Mapper for Task entity to/from DTOs.
/// </summary>
internal static class TaskMapper
{
    /// <summary>
    ///     Maps a Task entity to TaskResponse DTO.
    /// </summary>
    /// <param name="task">The Task entity.</param>
    /// <returns>TaskResponse DTO.</returns>
    public static TaskResponse ToResponse(Domain.Entities.Tasks.Task task)
    {
        return new TaskResponse
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
    ///     Maps a collection of Task entities to TaskListResponse DTO.
    /// </summary>
    /// <param name="tasks">The collection of Task entities.</param>
    /// <returns>TaskListResponse DTO.</returns>
    public static TaskListResponse ToListResponse(IEnumerable<Domain.Entities.Tasks.Task> tasks)
    {
        var taskList = tasks.ToList();
        return new TaskListResponse
        {
            Tasks = taskList.Select(ToResponse),
            TotalCount = taskList.Count
        };
    }

    /// <summary>
    ///     Creates a Task entity from CreateTaskCommand.
    ///     Note: CreatedAt/UpdatedAt are managed by the database via EF Core configuration.
    /// </summary>
    /// <param name="command">The CreateTaskCommand.</param>
    /// <returns>A new Task entity.</returns>
    public static Domain.Entities.Tasks.Task FromCreateCommand(CreateTaskCommand command)
    {
        return new Domain.Entities.Tasks.Task
        {
            Id = Guid.NewGuid(),
            Text = command.Text,
            IsCompleted = false,
            CompletedAt = null
        };
    }

    /// <summary>
    ///     Creates a new Task entity from UpdateTaskCommand with the existing ID.
    ///     Note: Since Task uses init-only properties for Text, we create a new instance.
    ///     EF Core will track and update the existing entity.
    /// </summary>
    /// <param name="existingId">The existing Task entity ID.</param>
    /// <param name="command">The UpdateTaskCommand.</param>
    /// <param name="existingTask">The existing task to preserve IsCompleted and CompletedAt.</param>
    /// <returns>A new Task entity with updated values.</returns>
    public static Domain.Entities.Tasks.Task FromUpdateCommand(Guid existingId, UpdateTaskCommand command, Domain.Entities.Tasks.Task existingTask)
    {
        return new Domain.Entities.Tasks.Task
        {
            Id = existingId,
            Text = command.Text,
            IsCompleted = existingTask.IsCompleted,
            CompletedAt = existingTask.CompletedAt
        };
    }
}
