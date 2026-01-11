using FluentValidation;
using Nebula.Contracts.Repositories;
using Nebula.Contracts.Services.DailyTasks;
using Nebula.DataTransfer.Common;
using Nebula.DataTransfer.Contracts.DailyTasks;
using Nebula.Services.Mapping;

namespace Nebula.Services.DailyTasks;

/// <inheritdoc />
public sealed class DailyTaskService(
    IUnitOfWork unitOfWork,
    IValidator<CreateDailyTaskCommand> createValidator,
    IValidator<(Guid, UpdateDailyTaskCommand)> updateValidator)
    : IDailyTaskService
{
    private readonly IValidator<CreateDailyTaskCommand> _createValidator =
        createValidator ?? throw new ArgumentNullException(nameof(createValidator));

    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    private readonly IValidator<(Guid, UpdateDailyTaskCommand)> _updateValidator =
        updateValidator ?? throw new ArgumentNullException(nameof(updateValidator));

    /// <inheritdoc />
    public async Task<TypedResult<DailyTaskResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            var task = await _unitOfWork.DailyTasks.GetByIdAsync(id, cancellationToken);

            if (task == null)
            {
                return TypedResult<DailyTaskResponse>.Result()
                    .WithErrorMessage($"Task with ID '{id}' not found.");
            }

            var response = DailyTaskMapper.ToResponse(task);
            return TypedResult<DailyTaskResponse>.Result(response);
        }
        catch (Exception ex)
        {
            return TypedResult<DailyTaskResponse>.Result()
                .WithErrorMessage($"An error occurred while retrieving the task: {ex.Message}")
                .WithException(ex);
        }
    }

    /// <inheritdoc />
    public async Task<TypedResult<DailyTaskListResponse>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var tasks = await _unitOfWork.DailyTasks.GetAllAsync(cancellationToken);
            var response = DailyTaskMapper.ToListResponse(tasks);
            return TypedResult<DailyTaskListResponse>.Result(response);
        }
        catch (Exception ex)
        {
            return TypedResult<DailyTaskListResponse>.Result()
                .WithErrorMessage($"An error occurred while retrieving tasks: {ex.Message}")
                .WithException(ex);
        }
    }

    /// <inheritdoc />
    public async Task<TypedResult<DailyTaskListResponse>> GetByDateAsync(DateOnly date, CancellationToken cancellationToken = default)
    {
        try
        {
            var startOfDay = date.ToDateTime(TimeOnly.MinValue);
            var endOfDay = date.ToDateTime(TimeOnly.MaxValue);

            var tasks = await _unitOfWork.DailyTasks.FindAsync(
                t => t.CreatedAt >= startOfDay && t.CreatedAt <= endOfDay,
                cancellationToken);

            var response = DailyTaskMapper.ToListResponse(tasks);
            return TypedResult<DailyTaskListResponse>.Result(response);
        }
        catch (Exception ex)
        {
            return TypedResult<DailyTaskListResponse>.Result()
                .WithErrorMessage($"An error occurred while retrieving tasks for date {date}: {ex.Message}")
                .WithException(ex);
        }
    }

    /// <inheritdoc />
    public async Task<TypedResult<DailyTaskResponse>> CreateAsync(CreateDailyTaskCommand command,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var validationResult = await _createValidator.ValidateAsync(command, cancellationToken);
            if (!validationResult.IsValid)
            {
                var result = TypedResult<DailyTaskResponse>.Result();
                foreach (var error in validationResult.Errors) result.WithErrorMessage(error.ErrorMessage);
                return result;
            }

            var task = DailyTaskMapper.FromCreateCommand(command);

            await _unitOfWork.DailyTasks.AddAsync(task, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var response = DailyTaskMapper.ToResponse(task);
            return TypedResult<DailyTaskResponse>.Result(response);
        }
        catch (Exception ex)
        {
            return TypedResult<DailyTaskResponse>.Result()
                .WithErrorMessage($"An error occurred while creating the task: {ex.Message}")
                .WithException(ex);
        }
    }

    /// <inheritdoc />
    public async Task<TypedResult<DailyTaskResponse>> UpdateAsync(Guid id, UpdateDailyTaskCommand command,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var validationResult = await _updateValidator.ValidateAsync((id, command), cancellationToken);
            if (!validationResult.IsValid)
            {
                var result = TypedResult<DailyTaskResponse>.Result();
                foreach (var error in validationResult.Errors) result.WithErrorMessage(error.ErrorMessage);
                return result;
            }

            var existingTask = await _unitOfWork.DailyTasks.GetByIdAsync(id, cancellationToken);
            if (existingTask == null)
            {
                return TypedResult<DailyTaskResponse>.Result()
                    .WithErrorMessage($"Task with ID '{id}' not found.");
            }

            var updatedTask = DailyTaskMapper.FromUpdateCommand(id, command, existingTask);

            _unitOfWork.DailyTasks.Update(updatedTask);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var response = DailyTaskMapper.ToResponse(updatedTask);
            return TypedResult<DailyTaskResponse>.Result(response);
        }
        catch (Exception ex)
        {
            return TypedResult<DailyTaskResponse>.Result()
                .WithErrorMessage($"An error occurred while updating the task: {ex.Message}")
                .WithException(ex);
        }
    }

    /// <inheritdoc />
    public async Task<TypedResult<DailyTaskResponse>> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            var task = await _unitOfWork.DailyTasks.GetByIdAsync(id, cancellationToken);

            if (task == null)
            {
                return TypedResult<DailyTaskResponse>.Result()
                    .WithErrorMessage($"Task with ID '{id}' not found.");
            }

            _unitOfWork.DailyTasks.Delete(task);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var response = DailyTaskMapper.ToResponse(task);
            return TypedResult<DailyTaskResponse>.Result(response);
        }
        catch (Exception ex)
        {
            return TypedResult<DailyTaskResponse>.Result()
                .WithErrorMessage($"An error occurred while deleting the task: {ex.Message}")
                .WithException(ex);
        }
    }

    /// <inheritdoc />
    public async Task<TypedResult<DailyTaskResponse>> MarkAsCompletedAsync(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            var task = await _unitOfWork.DailyTasks.GetByIdAsync(id, cancellationToken);

            if (task == null)
            {
                return TypedResult<DailyTaskResponse>.Result()
                    .WithErrorMessage($"Task with ID '{id}' not found.");
            }

            task.IsCompleted = true;
            task.CompletedAt = DateTime.UtcNow;

            _unitOfWork.DailyTasks.Update(task);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var response = DailyTaskMapper.ToResponse(task);
            return TypedResult<DailyTaskResponse>.Result(response);
        }
        catch (Exception ex)
        {
            return TypedResult<DailyTaskResponse>.Result()
                .WithErrorMessage($"An error occurred while marking the task as completed: {ex.Message}")
                .WithException(ex);
        }
    }

    /// <inheritdoc />
    public async Task<TypedResult<DailyTaskResponse>> MarkAsIncompleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            var task = await _unitOfWork.DailyTasks.GetByIdAsync(id, cancellationToken);

            if (task == null)
            {
                return TypedResult<DailyTaskResponse>.Result()
                    .WithErrorMessage($"Task with ID '{id}' not found.");
            }

            task.IsCompleted = false;
            task.CompletedAt = null;

            _unitOfWork.DailyTasks.Update(task);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var response = DailyTaskMapper.ToResponse(task);
            return TypedResult<DailyTaskResponse>.Result(response);
        }
        catch (Exception ex)
        {
            return TypedResult<DailyTaskResponse>.Result()
                .WithErrorMessage($"An error occurred while marking the task as incomplete: {ex.Message}")
                .WithException(ex);
        }
    }
}
