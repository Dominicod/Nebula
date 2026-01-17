using FluentValidation;
using Nebula.Contracts.Repositories;
using Nebula.Contracts.Services.Tasks;
using Nebula.DataTransfer.Common;
using Nebula.DataTransfer.Contracts.Tasks;
using Nebula.Services.Common.Mapping;

namespace Nebula.Services.Tasks;

/// <inheritdoc />
public sealed class TaskService(
    IUnitOfWork unitOfWork,
    IValidator<CreateTaskCommand> createValidator)
    : ITaskService
{
    private readonly IValidator<CreateTaskCommand> _createValidator =
        createValidator ?? throw new ArgumentNullException(nameof(createValidator));

    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    /// <inheritdoc />
    public async Task<TaskResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var task = await _unitOfWork.Tasks.GetByIdAsync(id, cancellationToken);

        if (task == null) return null;

        return TaskMapper.ToResponse(task);
    }

    /// <inheritdoc />
    public async Task<TypedResult<TaskListResponse>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var tasks = await _unitOfWork.Tasks.GetAllAsync(cancellationToken);
            var response = TaskMapper.ToListResponse(tasks);
            return TypedResult<TaskListResponse>.Result(response);
        }
        catch (Exception ex)
        {
            return TypedResult<TaskListResponse>.Result()
                .WithErrorMessage($"An error occurred while retrieving tasks: {ex.Message}")
                .WithException(ex);
        }
    }

    /// <inheritdoc />
    public async Task<TypedResult<TaskListResponse>> GetByDateAsync(DateOnly date,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var startOfDay = date.ToDateTime(TimeOnly.MinValue);
            var endOfDay = date.ToDateTime(TimeOnly.MaxValue);

            var tasks = await _unitOfWork.Tasks.FindAsync(
                t => t.CreatedAt >= startOfDay && t.CreatedAt <= endOfDay,
                cancellationToken);

            var response = TaskMapper.ToListResponse(tasks);
            return TypedResult<TaskListResponse>.Result(response);
        }
        catch (Exception ex)
        {
            return TypedResult<TaskListResponse>.Result()
                .WithErrorMessage($"An error occurred while retrieving tasks for date {date}: {ex.Message}")
                .WithException(ex);
        }
    }

    /// <inheritdoc />
    public async Task<TypedResult<TaskResponse>> CreateAsync(CreateTaskCommand command,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var validationResult = await _createValidator.ValidateAsync(command, cancellationToken);
            if (!validationResult.IsValid)
            {
                var result = TypedResult<TaskResponse>.Result();
                foreach (var error in validationResult.Errors) result.WithErrorMessage(error.ErrorMessage);
                return result;
            }

            var task = TaskMapper.FromCreateCommand(command);

            await _unitOfWork.Tasks.AddAsync(task, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var response = TaskMapper.ToResponse(task);
            return TypedResult<TaskResponse>.Result(response);
        }
        catch (Exception ex)
        {
            return TypedResult<TaskResponse>.Result()
                .WithErrorMessage($"An error occurred while creating the task: {ex.Message}")
                .WithException(ex);
        }
    }

    /// <inheritdoc />
    public async Task<TypedResult<TaskResponse>> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            var task = await _unitOfWork.Tasks.GetByIdAsync(id, cancellationToken);

            if (task == null)
            {
                return TypedResult<TaskResponse>.Result()
                    .WithErrorMessage($"Task with ID '{id}' not found.");
            }

            _unitOfWork.Tasks.Delete(task);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var response = TaskMapper.ToResponse(task);
            return TypedResult<TaskResponse>.Result(response);
        }
        catch (Exception ex)
        {
            return TypedResult<TaskResponse>.Result()
                .WithErrorMessage($"An error occurred while deleting the task: {ex.Message}")
                .WithException(ex);
        }
    }

    /// <inheritdoc />
    public async Task<TypedResult<TaskResponse>> MarkAsCompletedAsync(Guid id,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var task = await _unitOfWork.Tasks.GetByIdAsync(id, cancellationToken);

            if (task == null)
            {
                return TypedResult<TaskResponse>.Result()
                    .WithErrorMessage($"Task with ID '{id}' not found.");
            }

            task.IsCompleted = true;
            task.CompletedAt = DateTime.UtcNow;

            _unitOfWork.Tasks.Update(task);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var response = TaskMapper.ToResponse(task);
            return TypedResult<TaskResponse>.Result(response);
        }
        catch (Exception ex)
        {
            return TypedResult<TaskResponse>.Result()
                .WithErrorMessage($"An error occurred while marking the task as completed: {ex.Message}")
                .WithException(ex);
        }
    }

    /// <inheritdoc />
    public async Task<TypedResult<TaskResponse>> MarkAsIncompleteAsync(Guid id,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var task = await _unitOfWork.Tasks.GetByIdAsync(id, cancellationToken);

            if (task == null)
            {
                return TypedResult<TaskResponse>.Result()
                    .WithErrorMessage($"Task with ID '{id}' not found.");
            }

            task.IsCompleted = false;
            task.CompletedAt = null;

            _unitOfWork.Tasks.Update(task);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var response = TaskMapper.ToResponse(task);
            return TypedResult<TaskResponse>.Result(response);
        }
        catch (Exception ex)
        {
            return TypedResult<TaskResponse>.Result()
                .WithErrorMessage($"An error occurred while marking the task as incomplete: {ex.Message}")
                .WithException(ex);
        }
    }
}