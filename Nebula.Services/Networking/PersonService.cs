using Nebula.Contracts.Repositories;
using Nebula.Contracts.Services.Networking;
using Nebula.DataTransfer.Common;
using Nebula.DataTransfer.Contracts.Networking;
using Nebula.Services.Mapping;

namespace Nebula.Services.Networking;

/// <inheritdoc />
public sealed class PersonService(IUnitOfWork unitOfWork) : IPersonService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    /// <inheritdoc />
    public async Task<TypedResult<PersonResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            var person = await _unitOfWork.Persons.GetByIdAsync(id, cancellationToken);

            if (person == null)
            {
                return TypedResult<PersonResponse>.Result()
                    .WithErrorMessage($"Person with ID '{id}' not found.");
            }

            var response = PersonMapper.ToResponse(person);
            return TypedResult<PersonResponse>.Result(response);
        }
        catch (Exception ex)
        {
            return TypedResult<PersonResponse>.Result()
                .WithErrorMessage($"An error occurred while retrieving the person: {ex.Message}")
                .WithException(ex);
        }
    }

    /// <inheritdoc />
    public async Task<TypedResult<PersonListResponse>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var persons = await _unitOfWork.Persons.GetAllAsync(cancellationToken);
            var response = PersonMapper.ToListResponse(persons);
            return TypedResult<PersonListResponse>.Result(response);
        }
        catch (Exception ex)
        {
            return TypedResult<PersonListResponse>.Result()
                .WithErrorMessage($"An error occurred while retrieving persons: {ex.Message}")
                .WithException(ex);
        }
    }

    /// <inheritdoc />
    public async Task<TypedResult<PersonResponse>> CreateAsync(CreatePersonCommand command,
        CancellationToken cancellationToken = default)
    {
        try
        {
            // Check if a person with the same name already exists (business rule)
            var existingPersons = await _unitOfWork.Persons
                .FindAsync(i => i.FirstName == command.FirstName && i.LastName == command.LastName, cancellationToken);

            if (existingPersons.Any())
            {
                return TypedResult<PersonResponse>.Result()
                    .WithErrorMessage(
                        $"A person with the name '{command.FirstName} {command.LastName}' already exists.");
            }

            // Create new person entity
            var person = PersonMapper.FromCreateCommand(command);

            // Add to repository
            await _unitOfWork.Persons.AddAsync(person, cancellationToken);

            // Save changes
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var response = PersonMapper.ToResponse(person);
            return TypedResult<PersonResponse>.Result(response);
        }
        catch (Exception ex)
        {
            return TypedResult<PersonResponse>.Result()
                .WithErrorMessage($"An error occurred while creating the person: {ex.Message}")
                .WithException(ex);
        }
    }

    /// <inheritdoc />
    public async Task<TypedResult<PersonResponse>> UpdateAsync(Guid id, UpdatePersonCommand command,
        CancellationToken cancellationToken = default)
    {
        try
        {
            // Get existing person
            var existingPerson = await _unitOfWork.Persons.GetByIdAsync(id, cancellationToken);

            if (existingPerson == null)
            {
                return TypedResult<PersonResponse>.Result()
                    .WithErrorMessage($"Person with ID '{id}' not found.");
            }

            // Check if another person with the same name exists (business rule)
            var duplicatePersons = await _unitOfWork.Persons
                .FindAsync(i => i.FirstName == command.FirstName && i.LastName == command.LastName, cancellationToken);

            var hasDuplicate = duplicatePersons.Any(p => p.Id != id);
            if (hasDuplicate)
            {
                return TypedResult<PersonResponse>.Result()
                    .WithErrorMessage(
                        $"Another person with the name '{command.FirstName} {command.LastName}' already exists.");
            }

            // Create updated person entity
            var updatedPerson = PersonMapper.FromUpdateCommand(id, command);

            // Update in repository
            _unitOfWork.Persons.Update(updatedPerson);

            // Save changes
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var response = PersonMapper.ToResponse(updatedPerson);
            return TypedResult<PersonResponse>.Result(response);
        }
        catch (Exception ex)
        {
            return TypedResult<PersonResponse>.Result()
                .WithErrorMessage($"An error occurred while updating the person: {ex.Message}")
                .WithException(ex);
        }
    }

    /// <inheritdoc />
    public async Task<TypedResult<PersonResponse>> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            // Get existing person
            var person = await _unitOfWork.Persons.GetByIdAsync(id, cancellationToken);

            if (person == null)
            {
                return TypedResult<PersonResponse>.Result()
                    .WithErrorMessage($"Person with ID '{id}' not found.");
            }

            // Delete from repository
            _unitOfWork.Persons.Delete(person);

            // Save changes
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var response = PersonMapper.ToResponse(person);
            return TypedResult<PersonResponse>.Result(response);
        }
        catch (Exception ex)
        {
            return TypedResult<PersonResponse>.Result()
                .WithErrorMessage($"An error occurred while deleting the person: {ex.Message}")
                .WithException(ex);
        }
    }
}