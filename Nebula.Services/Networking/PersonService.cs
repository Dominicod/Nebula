using FluentValidation;
using Nebula.Contracts.Repositories;
using Nebula.Contracts.Services.Networking;
using Nebula.DataTransfer.Common;
using Nebula.DataTransfer.Contracts.Networking;
using Nebula.Services.Mapping;

namespace Nebula.Services.Networking;

/// <inheritdoc />
public sealed class PersonService(
    IUnitOfWork unitOfWork,
    IValidator<CreatePersonCommand> createValidator)
    : IPersonService
{
    private readonly IValidator<CreatePersonCommand> _createValidator =
        createValidator ?? throw new ArgumentNullException(nameof(createValidator));

    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    /// <inheritdoc />
    public async Task<PersonResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var person = await _unitOfWork.Persons.GetByIdAsync(id, cancellationToken);

        if (person == null)
        {
            return null;
        }

        return PersonMapper.ToResponse(person);
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
            var validationResult = await _createValidator.ValidateAsync(command, cancellationToken);
            if (!validationResult.IsValid)
            {
                var result = TypedResult<PersonResponse>.Result();
                foreach (var error in validationResult.Errors) result.WithErrorMessage(error.ErrorMessage);
                return result;
            }

            var person = PersonMapper.FromCreateCommand(command);

            await _unitOfWork.Persons.AddAsync(person, cancellationToken);
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
    public async Task<TypedResult<PersonResponse>> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            var person = await _unitOfWork.Persons.GetByIdAsync(id, cancellationToken);

            if (person == null)
            {
                return TypedResult<PersonResponse>.Result()
                    .WithErrorMessage($"Person with ID '{id}' not found.");
            }

            _unitOfWork.Persons.Delete(person);
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
