// Template: FluentValidation Validators
// Location: Nebula.Services/Validators/{Domain}/{Command}Validator.cs
// Example: Nebula.Services/Validators/Networking/CreatePersonCommandValidator.cs

using FluentValidation;
using Nebula.Contracts.Repositories;
using Nebula.DataTransfer.Contracts.{Domain};

namespace Nebula.Services.Validators.{Domain};

// ============================================================================
// CREATE VALIDATOR
// ============================================================================

/// <summary>
///     Validator for Create{EntityName}Command.
/// </summary>
public sealed class Create{EntityName}CommandValidator : AbstractValidator<Create{EntityName}Command>
{
    private readonly IUnitOfWork _unitOfWork;

    public Create{EntityName}CommandValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

        RuleFor(x => x.PropertyName)
            .NotEmpty()
            .WithMessage("Property name is required.")
            .MaximumLength(100)
            .WithMessage("Property name cannot exceed 100 characters.")
            .Must(BeValidFormat)
            .WithMessage("Property name contains invalid characters.");

        // Async database validation
        RuleFor(x => x)
            .MustAsync(NotHaveDuplicate)
            .WithMessage(x => $"A {EntityName} with this property already exists.");
    }

    /// <summary>
    ///     Validates format/pattern (synchronous).
    /// </summary>
    private static bool BeValidFormat(string value)
    {
        return !string.IsNullOrWhiteSpace(value) &&
               value.All(c => char.IsLetterOrDigit(c) || c == ' ');
    }

    /// <summary>
    ///     Validates uniqueness against database (asynchronous).
    /// </summary>
    private async Task<bool> NotHaveDuplicate(Create{EntityName}Command command, CancellationToken cancellationToken)
    {
        var existing = await _unitOfWork.{EntityName}s
            .FindAsync(e => e.PropertyName == command.PropertyName, cancellationToken);

        return !existing.Any();
    }
}

// ============================================================================
// UPDATE VALIDATOR
// ============================================================================

/// <summary>
///     Validator for Update{EntityName}Command.
///     Validates the command along with the entity ID being updated.
/// </summary>
public sealed class Update{EntityName}CommandValidator : AbstractValidator<(Guid Id, Update{EntityName}Command Command)>
{
    private readonly IUnitOfWork _unitOfWork;

    public Update{EntityName}CommandValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("{EntityName} ID is required.")
            .MustAsync(EntityExists)
            .WithMessage(x => $"{EntityName} with ID '{x.Id}' not found.");

        RuleFor(x => x.Command.PropertyName)
            .NotEmpty()
            .WithMessage("Property name is required.")
            .MaximumLength(100)
            .WithMessage("Property name cannot exceed 100 characters.")
            .Must(BeValidFormat)
            .WithMessage("Property name contains invalid characters.");

        RuleFor(x => x)
            .MustAsync(NotHaveDuplicate)
            .WithMessage(x => $"Another {EntityName} with this property already exists.");
    }

    private static bool BeValidFormat(string value)
    {
        return !string.IsNullOrWhiteSpace(value) &&
               value.All(c => char.IsLetterOrDigit(c) || c == ' ');
    }

    /// <summary>
    ///     Validates that the entity exists.
    /// </summary>
    private async Task<bool> EntityExists(Guid id, CancellationToken cancellationToken)
    {
        return await _unitOfWork.{EntityName}s.ExistsAsync(id, cancellationToken);
    }

    /// <summary>
    ///     Validates that no other entity has duplicate property.
    ///     Excludes the entity being updated from the check.
    /// </summary>
    private async Task<bool> NotHaveDuplicate((Guid Id, Update{EntityName}Command Command) context,
        CancellationToken cancellationToken)
    {
        var existing = await _unitOfWork.{EntityName}s
            .FindAsync(e => e.PropertyName == context.Command.PropertyName, cancellationToken);

        // Allow if no duplicates exist, or if the only match is the entity being updated
        return existing.All(e => e.Id == context.Id);
    }
}

// Notes:
// - CreateValidator: Validates CreateCommand
// - UpdateValidator: Validates tuple (Guid Id, UpdateCommand Command)
// - Use MustAsync for database checks (uniqueness, existence)
// - Use Must for synchronous format/pattern validation
// - Inject IUnitOfWork for database access
// - Register explicitly in DependencyInjection.Services.cs:
//   services.AddScoped<IValidator<Create{EntityName}Command>, Create{EntityName}CommandValidator>();
//   services.AddScoped<IValidator<(Guid, Update{EntityName}Command)>, Update{EntityName}CommandValidator>();
