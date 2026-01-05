using FluentValidation;
using Nebula.Contracts.Repositories;
using Nebula.DataTransfer.Contracts.Networking;

namespace Nebula.Services.Validators.Networking;

/// <summary>
///     Validator for UpdatePersonCommand.
///     Validates the command along with the person ID being updated.
/// </summary>
public sealed class UpdatePersonCommandValidator : AbstractValidator<(Guid Id, UpdatePersonCommand Command)>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdatePersonCommandValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Person ID is required.")
            .MustAsync(PersonExists)
            .WithMessage(x => $"Person with ID '{x.Id}' not found.");

        RuleFor(x => x.Command.FirstName)
            .NotEmpty()
            .WithMessage("First name is required.")
            .MaximumLength(100)
            .WithMessage("First name cannot exceed 100 characters.")
            .Must(BeValidName)
            .WithMessage("First name contains invalid characters.");

        RuleFor(x => x.Command.LastName)
            .NotEmpty()
            .WithMessage("Last name is required.")
            .MaximumLength(100)
            .WithMessage("Last name cannot exceed 100 characters.")
            .Must(BeValidName)
            .WithMessage("Last name contains invalid characters.");

        RuleFor(x => x)
            .MustAsync(NotHaveDuplicateName)
            .WithMessage(x =>
                $"Another person with the name '{x.Command.FirstName} {x.Command.LastName}' already exists.");
    }

    /// <summary>
    ///     Validates that the name contains only valid characters (letters, spaces, hyphens, apostrophes).
    /// </summary>
    private static bool BeValidName(string name)
    {
        return !string.IsNullOrWhiteSpace(name) &&
               // Allow letters (including Unicode letters), spaces, hyphens, and apostrophes
               name.All(c => char.IsLetter(c) || c == ' ' || c == '-' || c == '\'');
    }

    /// <summary>
    ///     Validates that the person exists.
    /// </summary>
    private async Task<bool> PersonExists(Guid id, CancellationToken cancellationToken)
    {
        return await _unitOfWork.Persons.ExistsAsync(id, cancellationToken);
    }

    /// <summary>
    ///     Validates that no other person with the same full name already exists.
    /// </summary>
    private async Task<bool> NotHaveDuplicateName((Guid Id, UpdatePersonCommand Command) context,
        CancellationToken cancellationToken)
    {
        var existingPersons = await _unitOfWork.Persons
            .FindAsync(p => p.FirstName == context.Command.FirstName && p.LastName == context.Command.LastName,
                cancellationToken);

        // Allow if no duplicates exist, or if the only match is the person being updated
        return existingPersons.All(p => p.Id == context.Id);
    }
}