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
}
