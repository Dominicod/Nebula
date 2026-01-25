using FluentValidation;
using Nebula.DataTransfer.Contracts.ActionItemTypes;

namespace Nebula.Services.Common.Validators.ActionItemTypes;

/// <summary>
///     Validator for CreateActionItemTypeCommand.
/// </summary>
public sealed class CreateActionItemTypeCommandValidator : AbstractValidator<CreateActionItemTypeCommand>
{
    public CreateActionItemTypeCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required.")
            .MaximumLength(200)
            .WithMessage("Name cannot exceed 200 characters.");
    }
}
