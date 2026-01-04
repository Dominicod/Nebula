using Nebula.Services.Common;
using Nebula.Services.Contracts.Networking;
using Nebula.Services.DTOs.Networking;
using Nebula.Services.Repositories.Networking;

namespace Nebula.Services.Networking;

/// <inheritdoc/>
public sealed class PersonService(IPersonRepository personRepository) : IPersonService
{
    /// <inheritdoc />
    public async Task<TypedResult<PersonListResponse>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var persons = await personRepository.GetAllAsync(cancellationToken);
            var personResponses = persons.Select(MapToResponse).ToList();

            var response = new PersonListResponse
            {
                Persons = personResponses,
                TotalCount = personResponses.Count
            };

            return TypedResult<PersonListResponse>.Success(response);
        }
        catch (Exception ex)
        {
            return TypedResult<PersonListResponse>.Exception(ex, "Failed to retrieve persons");
        }
    }

    /// <inheritdoc />
    public async Task<TypedResult<PersonResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            var person = await personRepository.GetByIdAsync(id, cancellationToken);

            if (person == null)
                return TypedResult<PersonResponse>.Failure("Person not found", "NOT_FOUND");

            return TypedResult<PersonResponse>.Success(MapToResponse(person));
        }
        catch (Exception ex)
        {
            return TypedResult<PersonResponse>.Exception(ex, "Failed to retrieve person");
        }
    }

    /// <inheritdoc />
    public async Task<TypedResult<PersonResponse>> CreateAsync(CreatePersonRequest request, CancellationToken cancellationToken = default)
    {
        try
        {
            var person = await personRepository.CreateAsync(
                request.FirstName,
                request.LastName,
                cancellationToken);

            return TypedResult<PersonResponse>.Success(MapToResponse(person));
        }
        catch (Exception ex)
        {
            return TypedResult<PersonResponse>.Exception(ex, "Failed to create person");
        }
    }

    /// <inheritdoc />
    public async Task<TypedResult<PersonResponse>> UpdateAsync(Guid id, UpdatePersonRequest request, CancellationToken cancellationToken = default)
    {
        try
        {
            var person = await personRepository.UpdateAsync(
                id,
                request.FirstName,
                request.LastName,
                cancellationToken);

            if (person == null)
                return TypedResult<PersonResponse>.Failure("Person not found", "NOT_FOUND");

            return TypedResult<PersonResponse>.Success(MapToResponse(person));
        }
        catch (Exception ex)
        {
            return TypedResult<PersonResponse>.Exception(ex, "Failed to update person");
        }
    }

    /// <inheritdoc />
    public async Task<TypedResult<PersonResponse>> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            var deleted = await personRepository.DeleteAsync(id, cancellationToken);

            if (!deleted)
                return TypedResult<PersonResponse>.Failure("Person not found", "NOT_FOUND");

            // For delete, we return an empty success response
            return TypedResult<PersonResponse>.Success(new PersonResponse
            {
                Id = id,
                FirstName = string.Empty,
                LastName = string.Empty,
                CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow
            });
        }
        catch (Exception ex)
        {
            return TypedResult<PersonResponse>.Exception(ex, "Failed to delete person");
        }
    }

    /// <summary>
    /// Maps a repository DTO to an API response contract.
    /// </summary>
    private static PersonResponse MapToResponse(PersonDto dto)
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
