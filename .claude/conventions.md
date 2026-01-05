# Nebula Coding Conventions

## C# 13 Features

**Primary Constructors**
```csharp
public sealed class PersonService(IUnitOfWork unitOfWork, IValidator<CreatePersonCommand> createValidator)
    : IPersonService
{
    // Parameters become private fields automatically
}
```

**Field Keyword (Lazy Initialization)**
```csharp
public IPersonRepository Persons => field ??= new PersonRepository(_context);
```

**Required Properties**
```csharp
public required string FirstName { get; init; }
```

## Naming Conventions

- **Entities**: PascalCase, singular (`Person`, `Company`)
- **Interfaces**: PascalCase with `I` prefix (`IPersonService`, `IRepository<T>`)
- **Private fields**: `_camelCase` prefix
- **DTOs**: Descriptive suffixes (`CreatePersonCommand`, `PersonResponse`, `PersonListResponse`)
- **Validators**: `{Command}Validator` (`CreatePersonCommandValidator`)

## Project Structure

**Layer Responsibilities**:
- `Nebula.Domain` - Entities only (no logic)
- `Nebula.Contracts.*` - Interfaces for cross-layer contracts
- `Nebula.Infrastructure` - EF Core, Repositories, UnitOfWork (mark `internal sealed`)
- `Nebula.Services` - Business logic, validators, mapping
- `Nebula.DataTransfer` - DTOs (use `sealed record` for immutability)
- `Nebula.API` - Routes, minimal APIs

## Key Patterns

**TypedResult Pattern**
```csharp
// Success
return TypedResult<PersonResponse>.Result(response);

// Failure
return TypedResult<PersonResponse>.Result()
    .WithErrorMessage("Person not found.");
```

**Repository Pattern**
- Base interface: `IRepository<TEntity, TId>`
- Specific interface: `IPersonRepository : IRepository<Person, Guid>`
- Implementation: `internal sealed class PersonRepository : Repository<Person, Guid>, IPersonRepository`
- Auto-registered via assembly scanning (no manual DI registration needed)

**Unit of Work Pattern**
```csharp
await _unitOfWork.Persons.AddAsync(person, cancellationToken);
await _unitOfWork.SaveChangesAsync(cancellationToken);
```

**Manual Mapping**
```csharp
internal static class PersonMapper
{
    public static PersonResponse ToResponse(Person person) => new() { ... };
    public static Person FromCreateCommand(CreatePersonCommand command) => new() { ... };
}
```

## Validation

**FluentValidation Location**: `Nebula.Services/Validators/{Domain}/{Command}Validator.cs`

**Validator Registration**: Explicit + assembly scanning in `DependencyInjection.Services.cs`:
```csharp
services.AddScoped<IValidator<CreatePersonCommand>, CreatePersonCommandValidator>();
services.AddValidatorsFromAssemblyContaining<CreatePersonCommandValidator>();
```

**Async Database Validation**:
```csharp
RuleFor(x => x)
    .MustAsync(NotHaveDuplicateName)
    .WithMessage(x => $"Person '{x.FirstName} {x.LastName}' already exists.");
```

**Update Validators**: Use tuple pattern `(Guid Id, UpdatePersonCommand Command)` to validate both ID and command.

## Service Layer

**Error Handling**: Never throw exceptions from services - use TypedResult:
```csharp
try
{
    var validationResult = await _validator.ValidateAsync(command, cancellationToken);
    if (!validationResult.IsValid)
    {
        var result = TypedResult<PersonResponse>.Result();
        foreach (var error in validationResult.Errors)
            result.WithErrorMessage(error.ErrorMessage);
        return result;
    }
    // ... business logic
}
catch (Exception ex)
{
    return TypedResult<PersonResponse>.Result()
        .WithErrorMessage($"An error occurred: {ex.Message}")
        .WithException(ex);
}
```

## EF Core

**Read Operations**: Always use `AsNoTracking()`:
```csharp
return await _dbSet.AsNoTracking().ToListAsync(cancellationToken);
```

**Configuration**: Extend `BaseEntityConfiguration<TEntity, TId>` for entity configurations.

**Timestamps**: `CreatedAt`/`UpdatedAt` managed by database defaults - don't set manually in mappers.

## DTOs

**Use sealed records**:
```csharp
public sealed record CreatePersonCommand
{
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
}
```

**Response DTOs**: Include Id and timestamps where relevant.

## Minimal APIs

**Route Organization**: `Nebula.API/Routes/{Domain}/{Entity}Routes.cs`

**Route Registration Pattern**:
```csharp
public static class PersonRoutes
{
    public static void MapPersonRoutes(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/persons").WithTags("Persons");

        group.MapGet("/{id:guid}", GetPersonById);
        group.MapPost("/", CreatePerson);
        // ...
    }

    private static async Task<IResult> GetPersonById(...)
    {
        var result = await personService.GetByIdAsync(id, cancellationToken);
        return result.Success ? Results.Ok(result.Data) : Results.NotFound(result.ErrorMessages);
    }
}
```

## Visibility Modifiers

- **Infrastructure**: `internal sealed` (repositories, UnitOfWork)
- **Services**: `public sealed`
- **Validators**: `public sealed`
- **Mappers**: `internal static`
- **DTOs**: `public sealed record`
- **Entities**: `public` with `init` properties

## Don'ts

- Don't use AutoMapper (manual mapping only)
- Don't create controllers (Minimal APIs only)
- Don't manually register repositories (auto-scanned)
- Don't throw exceptions from services (use TypedResult)
- Don't skip validators for Create/Update operations
- Don't set `CreatedAt`/`UpdatedAt` in mappers
- Don't use tracking for read-only queries
