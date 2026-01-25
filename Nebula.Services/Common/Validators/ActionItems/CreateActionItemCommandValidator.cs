using FluentValidation;
using Nebula.DataTransfer.Contracts.ActionItems;

namespace Nebula.Services.Common.Validators.ActionItems;

/// <summary>
///     Validator for CreateActionItemCommand.
/// </summary>
public sealed class CreateActionItemCommandValidator : AbstractValidator<CreateActionItemCommand>
{
    public CreateActionItemCommandValidator()
    {
        RuleFor(x => x.Text)
            .NotEmpty()
            .WithMessage("Text is required.")
            .MaximumLength(2000)
            .WithMessage("Text cannot exceed 2000 characters.");

        RuleFor(x => x.ActionItemTypeId)
            .NotEmpty()
            .WithMessage("ActionItemTypeId is required.");
    }
}