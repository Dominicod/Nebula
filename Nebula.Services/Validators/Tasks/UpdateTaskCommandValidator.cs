using FluentValidation;
using Nebula.Contracts.Repositories;
using Nebula.DataTransfer.Contracts.Tasks;

namespace Nebula.Services.Validators.Tasks;

/// <summary>
///     Validator for UpdateTaskCommand.
///     Validates the command along with the task ID being updated.
/// </summary>
public sealed class UpdateTaskCommandValidator : AbstractValidator<(Guid Id, UpdateTaskCommand Command)>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateTaskCommandValidator(IUnitOfWork unitOfWork)
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
        return await _unitOfWork.Tasks.ExistsAsync(id, cancellationToken);
    }
}
