using FluentValidation;
using Nebula.DataTransfer.Contracts.Tasks;

namespace Nebula.Services.Validators.Tasks;

/// <summary>
///     Validator for CreateTaskCommand.
/// </summary>
public sealed class CreateTaskCommandValidator : AbstractValidator<CreateTaskCommand>
{
    public CreateTaskCommandValidator()
    {
        RuleFor(x => x.Text)
            .NotEmpty()
            .WithMessage("Text is required.")
            .MaximumLength(2000)
            .WithMessage("Text cannot exceed 2000 characters.");
    }
}
