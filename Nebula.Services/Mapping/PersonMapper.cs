using Nebula.DataTransfer.Contracts.Networking;
using Nebula.Domain.Entities.Networking;

namespace Nebula.Services.Mapping;

/// <summary>
///     Mapper for Person entity to/from DTOs.
/// </summary>
internal static class PersonMapper
{
    /// <summary>
    ///     Maps a Person entity to PersonResponse DTO.
    /// </summary>
    /// <param name="person">The Person entity.</param>
    /// <returns>PersonResponse DTO.</returns>
    public static PersonResponse ToResponse(Person person)
    {
        return new PersonResponse
        {
            Id = person.Id,
            FirstName = person.FirstName,
            LastName = person.LastName,
            CreatedAt = person.CreatedAt,
            UpdatedAt = person.UpdatedAt
        };
    }

    /// <summary>
    ///     Maps a collection of Person entities to PersonListResponse DTO.
    /// </summary>
    /// <param name="persons">The collection of Person entities.</param>
    /// <returns>PersonListResponse DTO.</returns>
    public static PersonListResponse ToListResponse(IEnumerable<Person> persons)
    {
        var personList = persons.ToList();
        return new PersonListResponse
        {
            Persons = personList.Select(ToResponse),
            TotalCount = personList.Count
        };
    }

    /// <summary>
    ///     Creates a Person entity from CreatePersonCommand.
    ///     Note: Since Person uses init-only properties, we cannot set CreatedAt/UpdatedAt.
    ///     These will be managed by the database via EF Core configuration.
    /// </summary>
    /// <param name="command">The CreatePersonCommand.</param>
    /// <returns>A new Person entity.</returns>
    public static Person FromCreateCommand(CreatePersonCommand command)
    {
        return new Person
        {
            Id = Guid.NewGuid(),
            FirstName = command.FirstName,
            LastName = command.LastName
        };
    }
}
