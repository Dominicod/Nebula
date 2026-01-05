using System.ComponentModel.DataAnnotations;

namespace Nebula.DataTransfer.Contracts.Networking;

/// <summary>
///     Request contract for creating a new person.
/// </summary>
public sealed record CreatePersonCommand : ICommand
{
    /// <summary>
    ///     Gets the first name of the person.
    /// </summary>
    [Required(ErrorMessage = "First name is required")]
    [MaxLength(100, ErrorMessage = "First name cannot exceed 100 characters")]
    public required string FirstName { get; init; }

    /// <summary>
    ///     Gets the last name of the person.
    /// </summary>
    [Required(ErrorMessage = "Last name is required")]
    [MaxLength(100, ErrorMessage = "Last name cannot exceed 100 characters")]
    public required string LastName { get; init; }
}