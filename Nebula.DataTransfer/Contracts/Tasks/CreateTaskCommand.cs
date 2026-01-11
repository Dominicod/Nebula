using System.ComponentModel.DataAnnotations;

namespace Nebula.DataTransfer.Contracts.Tasks;

/// <summary>
///     Request contract for creating a new task.
/// </summary>
public sealed record CreateTaskCommand : ICommand
{
    /// <summary>
    ///     Gets the text content of the task.
    /// </summary>
    [Required(ErrorMessage = "Text is required")]
    [MaxLength(2000, ErrorMessage = "Text cannot exceed 2000 characters")]
    public required string Text { get; init; }
}
