using FluentValidation;
using Nebula.Contracts.Repositories;
using Nebula.Contracts.Services.ActionItemTypes;
using Nebula.DataTransfer.Common;
using Nebula.DataTransfer.Contracts.ActionItemTypes;
using Nebula.Services.Common.Mapping;

namespace Nebula.Services.ActionItemTypes;

/// <inheritdoc />
public sealed class ActionItemTypeService(
    IUnitOfWork unitOfWork,
    IValidator<CreateActionItemTypeCommand> createValidator)
    : IActionItemTypeService
{
    private readonly IValidator<CreateActionItemTypeCommand> _createValidator =
        createValidator ?? throw new ArgumentNullException(nameof(createValidator));

    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    /// <inheritdoc />
    public async Task<ActionItemTypeResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var actionItemType = await _unitOfWork.ActionItemTypes.GetByIdAsync(id, cancellationToken);

        if (actionItemType == null) return null;

        return ActionItemTypeMapper.ToResponse(actionItemType);
    }

    /// <inheritdoc />
    public async Task<TypedResult<ActionItemTypeListResponse>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var actionItemTypes = await _unitOfWork.ActionItemTypes.GetAllAsync(cancellationToken);
            var response = ActionItemTypeMapper.ToListResponse(actionItemTypes);
            return TypedResult<ActionItemTypeListResponse>.Result(response);
        }
        catch (Exception ex)
        {
            return TypedResult<ActionItemTypeListResponse>.Result()
                .WithErrorMessage($"An error occurred while retrieving action item types: {ex.Message}")
                .WithException(ex);
        }
    }

    /// <inheritdoc />
    public async Task<TypedResult<ActionItemTypeResponse>> CreateAsync(CreateActionItemTypeCommand command,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var validationResult = await _createValidator.ValidateAsync(command, cancellationToken);
            if (!validationResult.IsValid)
            {
                var result = TypedResult<ActionItemTypeResponse>.Result();
                foreach (var error in validationResult.Errors) result.WithErrorMessage(error.ErrorMessage);
                return result;
            }

            var actionItemType = ActionItemTypeMapper.FromCreateCommand(command);

            await _unitOfWork.ActionItemTypes.AddAsync(actionItemType, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var response = ActionItemTypeMapper.ToResponse(actionItemType);
            return TypedResult<ActionItemTypeResponse>.Result(response);
        }
        catch (Exception ex)
        {
            return TypedResult<ActionItemTypeResponse>.Result()
                .WithErrorMessage($"An error occurred while creating the action item type: {ex.Message}")
                .WithException(ex);
        }
    }

    /// <inheritdoc />
    public async Task<TypedResult<ActionItemTypeResponse>> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            var actionItemType = await _unitOfWork.ActionItemTypes.GetByIdAsync(id, cancellationToken);

            if (actionItemType == null)
            {
                return TypedResult<ActionItemTypeResponse>.Result()
                    .WithErrorMessage($"ActionItemType with ID '{id}' not found.");
            }

            _unitOfWork.ActionItemTypes.Delete(actionItemType);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var response = ActionItemTypeMapper.ToResponse(actionItemType);
            return TypedResult<ActionItemTypeResponse>.Result(response);
        }
        catch (Exception ex)
        {
            return TypedResult<ActionItemTypeResponse>.Result()
                .WithErrorMessage($"An error occurred while deleting the action item type: {ex.Message}")
                .WithException(ex);
        }
    }
}
