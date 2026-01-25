using System.ComponentModel.DataAnnotations;
using Nebula.Contracts.Common;

namespace Nebula.DataTransfer.Contracts.ActionItemTypes;

/// <summary>
///     Request contract for creating a new action item type.
/// </summary>
public sealed record CreateActionItemTypeCommand : ICommand
{
    /// <summary>
    ///     Gets the name of the action item type.
    /// </summary>
    [Required(ErrorMessage = "Name is required")]
    [MaxLength(200, ErrorMessage = "Name cannot exceed 200 characters")]
    public required string Name { get; init; }
}
