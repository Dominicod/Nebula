using Nebula.Services.Contracts.Networking;
using Nebula.Architecture.Repositories.Networking;

namespace Nebula.Services.Networking;


/// <inheritdoc/>
public sealed class PersonService(IPersonRepository personRepository) : IPersonService
{
    /// <inheritdoc />
    public async Task<IEnumerable<PersonResponse>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var persons = await personRepository.GetAllAsync(cancellationToken);
        return persons.Select(MapToResponse);
    }

    /// <inheritdoc />
    public async Task<PersonResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var person = await personRepository.GetByIdAsync(id, cancellationToken);
        return person != null ? MapToResponse(person) : null;
    }

    /// <inheritdoc />
    public async Task<PersonResponse> CreateAsync(CreatePersonRequest request, CancellationToken cancellationToken = default)
    {
        var person = await personRepository.CreateAsync(
            request.FirstName,
            request.LastName,
            cancellationToken);

        return MapToResponse(person);
    }

    /// <inheritdoc />
    public async Task<PersonResponse?> UpdateAsync(Guid id, UpdatePersonRequest request, CancellationToken cancellationToken = default)
    {
        var person = await personRepository.UpdateAsync(
            id,
            request.FirstName,
            request.LastName,
            cancellationToken);

        return person != null ? MapToResponse(person) : null;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await personRepository.DeleteAsync(id, cancellationToken);
    }

    /// <summary>
    /// Maps a repository DTO to an API response contract.
    /// </summary>
    private static PersonResponse MapToResponse(Architecture.DTOs.Networking.PersonDto dto)
    {
        return new PersonResponse
        {
            Id = dto.Id,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            CreatedAt = dto.CreatedAt,
            UpdatedAt = dto.UpdatedAt
        };
    }
}