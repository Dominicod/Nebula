// Template: Service Interface and Implementation
// Interface Location: Contracts/Nebula.Contracts.Services/{Domain}/I{EntityName}Service.cs
// Implementation Location: Nebula.Services/{Domain}/{EntityName}Service.cs

// ============================================================================
// INTERFACE (in Nebula.Contracts.Services)
// ============================================================================

using Nebula.DataTransfer.Common;
using Nebula.DataTransfer.Contracts.{Domain};

namespace Nebula.Contracts.Services.{Domain};

/// <summary>
///     Service interface for {EntityName} operations.
/// </summary>
public interface I{EntityName}Service
{
    Task<TypedResult<{EntityName}Response>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<TypedResult<{EntityName}ListResponse>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<TypedResult<{EntityName}Response>> CreateAsync(Create{EntityName}Command command, CancellationToken cancellationToken = default);
    Task<TypedResult<{EntityName}Response>> UpdateAsync(Guid id, Update{EntityName}Command command, CancellationToken cancellationToken = default);
    Task<TypedResult<{EntityName}Response>> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}

// ============================================================================
// IMPLEMENTATION (in Nebula.Services)
// ============================================================================

using FluentValidation;
using Nebula.Contracts.Repositories;
using Nebula.Contracts.Services.{Domain};
using Nebula.DataTransfer.Common;
using Nebula.DataTransfer.Contracts.{Domain};
using Nebula.Services.Mapping;

namespace Nebula.Services.{Domain};

/// <inheritdoc />
public sealed class {EntityName}Service(
    IUnitOfWork unitOfWork,
    IValidator<Create{EntityName}Command> createValidator,
    IValidator<(Guid, Update{EntityName}Command)> updateValidator)
    : I{EntityName}Service
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    private readonly IValidator<Create{EntityName}Command> _createValidator =
        createValidator ?? throw new ArgumentNullException(nameof(createValidator));
    private readonly IValidator<(Guid, Update{EntityName}Command)> _updateValidator =
        updateValidator ?? throw new ArgumentNullException(nameof(updateValidator));

    /// <inheritdoc />
    public async Task<TypedResult<{EntityName}Response>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            var entity = await _unitOfWork.{EntityName}s.GetByIdAsync(id, cancellationToken);

            if (entity == null)
            {
                return TypedResult<{EntityName}Response>.Result()
                    .WithErrorMessage($"{EntityName} with ID '{id}' not found.");
            }

            var response = {EntityName}Mapper.ToResponse(entity);
            return TypedResult<{EntityName}Response>.Result(response);
        }
        catch (Exception ex)
        {
            return TypedResult<{EntityName}Response>.Result()
                .WithErrorMessage($"An error occurred while retrieving the {EntityName}: {ex.Message}")
                .WithException(ex);
        }
    }

    /// <inheritdoc />
    public async Task<TypedResult<{EntityName}Response>> CreateAsync(Create{EntityName}Command command,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var validationResult = await _createValidator.ValidateAsync(command, cancellationToken);
            if (!validationResult.IsValid)
            {
                var result = TypedResult<{EntityName}Response>.Result();
                foreach (var error in validationResult.Errors)
                    result.WithErrorMessage(error.ErrorMessage);
                return result;
            }

            var entity = {EntityName}Mapper.FromCreateCommand(command);

            await _unitOfWork.{EntityName}s.AddAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var response = {EntityName}Mapper.ToResponse(entity);
            return TypedResult<{EntityName}Response>.Result(response);
        }
        catch (Exception ex)
        {
            return TypedResult<{EntityName}Response>.Result()
                .WithErrorMessage($"An error occurred while creating the {EntityName}: {ex.Message}")
                .WithException(ex);
        }
    }

    // Implement UpdateAsync and DeleteAsync following the same pattern
}

// Notes:
// - Use primary constructor with IUnitOfWork and validators
// - Never throw exceptions - use TypedResult pattern
// - Always validate before persistence operations
// - Wrap in try-catch and return TypedResult with error details
// - Use mapper for entity/DTO conversion
// - Register in DependencyInjection.Services.cs
