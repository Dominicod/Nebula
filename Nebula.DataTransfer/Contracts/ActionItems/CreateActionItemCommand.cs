using System.ComponentModel.DataAnnotations;
using Nebula.Contracts.Common;

namespace Nebula.DataTransfer.Contracts.ActionItems;

/// <summary>
///     Request contract for creating a new actionItem.
/// </summary>
public sealed record CreateActionItemCommand : ICommand
{
    /// <summary>
    ///     Gets the text content of the actionItem.
    /// </summary>
    [Required(ErrorMessage = "Text is required")]
    [MaxLength(2000, ErrorMessage = "Text cannot exceed 2000 characters")]
    public required string Text { get; init; }
}