using FluentValidation;
using Nebula.Contracts.Repositories;
using Nebula.DataTransfer.Contracts.DailyTasks;

namespace Nebula.Services.Validators.DailyTasks;

/// <summary>
///     Validator for UpdateDailyTaskCommand.
///     Validates the command along with the task ID being updated.
/// </summary>
public sealed class UpdateDailyTaskCommandValidator : AbstractValidator<(Guid Id, UpdateDailyTaskCommand Command)>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateDailyTaskCommandValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Task ID is required.")
            .MustAsync(TaskExists)
            .WithMessage(x => $"Task with ID '{x.Id}' not found.");

        RuleFor(x => x.Command.Text)
            .NotEmpty()
            .WithMessage("Text is required.")
            .MaximumLength(2000)
            .WithMessage("Text cannot exceed 2000 characters.");
    }

    /// <summary>
    ///     Validates that the task exists.
    /// </summary>
    private async Task<bool> TaskExists(Guid id, CancellationToken cancellationToken)
    {
        return await _unitOfWork.DailyTasks.ExistsAsync(id, cancellationToken);
    }
}
