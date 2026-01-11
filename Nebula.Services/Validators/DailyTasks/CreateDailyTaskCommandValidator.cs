using FluentValidation;
using Nebula.DataTransfer.Contracts.DailyTasks;

namespace Nebula.Services.Validators.DailyTasks;

/// <summary>
///     Validator for CreateDailyTaskCommand.
/// </summary>
public sealed class CreateDailyTaskCommandValidator : AbstractValidator<CreateDailyTaskCommand>
{
    public CreateDailyTaskCommandValidator()
    {
        RuleFor(x => x.Text)
            .NotEmpty()
            .WithMessage("Text is required.")
            .MaximumLength(2000)
            .WithMessage("Text cannot exceed 2000 characters.");
    }
}
