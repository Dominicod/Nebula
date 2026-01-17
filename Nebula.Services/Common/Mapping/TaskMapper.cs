using Nebula.DataTransfer.Contracts.Tasks;
using Task = Nebula.Domain.Entities.Tasks.Task;

namespace Nebula.Services.Common.Mapping;

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
    public static TaskResponse ToResponse(Task task)
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
    public static TaskListResponse ToListResponse(IEnumerable<Task> tasks)
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
    public static Task FromCreateCommand(CreateTaskCommand command)
    {
        return new Task
        {
            Id = Guid.NewGuid(),
            Text = command.Text,
            IsCompleted = false,
            CompletedAt = null
        };
    }
}