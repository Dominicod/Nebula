using FluentValidation;
using Nebula.Contracts.Repositories;
using Nebula.DataTransfer.Contracts.Networking;

namespace Nebula.Services.Common.Validators.Networking;

/// <summary>
///     Validator for CreatePersonCommand.
/// </summary>
public sealed class CreatePersonCommandValidator : AbstractValidator<CreatePersonCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreatePersonCommandValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage("First name is required.")
            .MaximumLength(100)
            .WithMessage("First name cannot exceed 100 characters.")
            .Must(BeValidName)
            .WithMessage("First name contains invalid characters.");

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage("Last name is required.")
            .MaximumLength(100)
            .WithMessage("Last name cannot exceed 100 characters.")
            .Must(BeValidName)
            .WithMessage("Last name contains invalid characters.");

        RuleFor(x => x)
            .MustAsync(NotHaveDuplicateName)
            .WithMessage(x => $"A person with the name '{x.FirstName} {x.LastName}' already exists.");
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
    ///     Validates that no person with the same full name already exists.
    /// </summary>
    private async Task<bool> NotHaveDuplicateName(CreatePersonCommand command, CancellationToken cancellationToken)
    {
        var existingPersons = await _unitOfWork.Persons
            .FindAsync(p => p.FirstName == command.FirstName && p.LastName == command.LastName, cancellationToken);

        return !existingPersons.Any();
    }
}