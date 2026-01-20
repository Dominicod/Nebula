using FluentValidation;
using Nebula.Contracts.Repositories;
using Nebula.Contracts.Services.ActionItems;
using Nebula.DataTransfer.Common;
using Nebula.DataTransfer.Contracts.ActionItems;
using Nebula.Services.Common.Mapping;

namespace Nebula.Services.ActionItems;

/// <inheritdoc />
public sealed class ActionItemService(
    IUnitOfWork unitOfWork,
    IValidator<CreateActionItemCommand> createValidator)
    : IActionItemService
{
    private readonly IValidator<CreateActionItemCommand> _createValidator =
        createValidator ?? throw new ArgumentNullException(nameof(createValidator));

    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    /// <inheritdoc />
    public async Task<ActionItemResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var actionItem = await _unitOfWork.ActionItems.GetByIdAsync(id, cancellationToken);

        if (actionItem == null) return null;

        return ActionItemMapper.ToResponse(actionItem);
    }

    /// <inheritdoc />
    public async Task<TypedResult<ActionItemListResponse>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var actionItems = await _unitOfWork.ActionItems.GetAllAsync(cancellationToken);
            var response = ActionItemMapper.ToListResponse(actionItems);
            return TypedResult<ActionItemListResponse>.Result(response);
        }
        catch (Exception ex)
        {
            return TypedResult<ActionItemListResponse>.Result()
                .WithErrorMessage($"An error occurred while retrieving actionItems: {ex.Message}")
                .WithException(ex);
        }
    }

    /// <inheritdoc />
    public async Task<TypedResult<ActionItemListResponse>> GetByDateAsync(DateOnly date,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var startOfDay = date.ToDateTime(TimeOnly.MinValue);
            var endOfDay = date.ToDateTime(TimeOnly.MaxValue);

            var actionItems = await _unitOfWork.ActionItems.FindAsync(
                t => t.CreatedAt >= startOfDay && t.CreatedAt <= endOfDay,
                cancellationToken);

            var response = ActionItemMapper.ToListResponse(actionItems);
            return TypedResult<ActionItemListResponse>.Result(response);
        }
        catch (Exception ex)
        {
            return TypedResult<ActionItemListResponse>.Result()
                .WithErrorMessage($"An error occurred while retrieving actionItems for date {date}: {ex.Message}")
                .WithException(ex);
        }
    }

    /// <inheritdoc />
    public async Task<TypedResult<ActionItemResponse>> CreateAsync(CreateActionItemCommand command,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var validationResult = await _createValidator.ValidateAsync(command, cancellationToken);
            if (!validationResult.IsValid)
            {
                var result = TypedResult<ActionItemResponse>.Result();
                foreach (var error in validationResult.Errors) result.WithErrorMessage(error.ErrorMessage);
                return result;
            }

            var actionItem = ActionItemMapper.FromCreateCommand(command);

            await _unitOfWork.ActionItems.AddAsync(actionItem, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var response = ActionItemMapper.ToResponse(actionItem);
            return TypedResult<ActionItemResponse>.Result(response);
        }
        catch (Exception ex)
        {
            return TypedResult<ActionItemResponse>.Result()
                .WithErrorMessage($"An error occurred while creating the actionItem: {ex.Message}")
                .WithException(ex);
        }
    }

    /// <inheritdoc />
    public async Task<TypedResult<ActionItemResponse>> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            var actionItem = await _unitOfWork.ActionItems.GetByIdAsync(id, cancellationToken);

            if (actionItem == null)
            {
                return TypedResult<ActionItemResponse>.Result()
                    .WithErrorMessage($"ActionItem with ID '{id}' not found.");
            }

            _unitOfWork.ActionItems.Delete(actionItem);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var response = ActionItemMapper.ToResponse(actionItem);
            return TypedResult<ActionItemResponse>.Result(response);
        }
        catch (Exception ex)
        {
            return TypedResult<ActionItemResponse>.Result()
                .WithErrorMessage($"An error occurred while deleting the actionItem: {ex.Message}")
                .WithException(ex);
        }
    }

    /// <inheritdoc />
    public async Task<TypedResult<ActionItemResponse>> MarkAsCompletedAsync(Guid id,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var actionItem = await _unitOfWork.ActionItems.GetByIdAsync(id, cancellationToken);

            if (actionItem == null)
            {
                return TypedResult<ActionItemResponse>.Result()
                    .WithErrorMessage($"ActionItem with ID '{id}' not found.");
            }

            actionItem.IsCompleted = true;
            actionItem.CompletedAt = DateTime.UtcNow;

            _unitOfWork.ActionItems.Update(actionItem);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var response = ActionItemMapper.ToResponse(actionItem);
            return TypedResult<ActionItemResponse>.Result(response);
        }
        catch (Exception ex)
        {
            return TypedResult<ActionItemResponse>.Result()
                .WithErrorMessage($"An error occurred while marking the actionItem as completed: {ex.Message}")
                .WithException(ex);
        }
    }

    /// <inheritdoc />
    public async Task<TypedResult<ActionItemResponse>> MarkAsIncompleteAsync(Guid id,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var actionItem = await _unitOfWork.ActionItems.GetByIdAsync(id, cancellationToken);

            if (actionItem == null)
            {
                return TypedResult<ActionItemResponse>.Result()
                    .WithErrorMessage($"ActionItem with ID '{id}' not found.");
            }

            actionItem.IsCompleted = false;
            actionItem.CompletedAt = null;

            _unitOfWork.ActionItems.Update(actionItem);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var response = ActionItemMapper.ToResponse(actionItem);
            return TypedResult<ActionItemResponse>.Result(response);
        }
        catch (Exception ex)
        {
            return TypedResult<ActionItemResponse>.Result()
                .WithErrorMessage($"An error occurred while marking the actionItem as incomplete: {ex.Message}")
                .WithException(ex);
        }
    }
}