# Nebula Project - Agent Quick Reference

## Quick Start for AI Agents

This directory contains concise templates and patterns for the Nebula project. Use these files as references when creating new components.

### Project Stack
- **.NET 10.0** (C# 13)
- **Entity Framework Core 10.0.1** + SQL Server
- **Minimal APIs** (no controllers)
- **FluentValidation 12.1.1**
- **Clean Architecture** (DDD)

### Key Patterns
- **Repository + Unit of Work** for data access
- **TypedResult<T>** for error handling
- **Manual mapping** (no AutoMapper)
- **FluentValidation** for business rules
- **Primary constructors** and `field` keyword

### File Structure
```
.claude/
├── README.md              # This file
├── templates/
│   ├── entity.cs          # Domain entity template
│   ├── repository.cs      # Repository interface/implementation
│   ├── service.cs         # Service with validators
│   ├── routes.cs          # API route handlers
│   └── validator.cs       # FluentValidation rules
└── conventions.md         # Coding standards (1 page)
```

### Creating New Features

**Pattern**: For a new entity called `Company`:

1. **Domain**: `Nebula.Domain/Entities/Networking/Company.cs` → See `templates/entity.cs`
2. **Repository**: See `templates/repository.cs` (auto-registered via assembly scanning)
3. **DTOs**: Create `CreateCompanyCommand`, `UpdateCompanyCommand`, `CompanyResponse`
4. **Service**: See `templates/service.cs`
5. **Validators**: See `templates/validator.cs`
6. **Routes**: See `templates/routes.cs`
7. **EF Config**: Extend `BaseEntityConfiguration<Company, Guid>`

### Quick Commands (Windows)

```cmd
# Build
dotnet build

# Create migration
dotnet ef migrations add AddCompany --project Nebula.Infrastructure --startup-project Nebula.API

# Update database
dotnet ef database update --project Nebula.Infrastructure --startup-project Nebula.API

# Run
dotnet run --project Nebula.API
```

### Reference Existing Code

Instead of reading lengthy documentation, **examine existing implementations**:
- **Entity**: `Nebula.Domain/Entities/Networking/Person.cs`
- **Repository**: `Nebula.Infrastructure/Data/Repositories/Networking/PersonRepository.cs`
- **Service**: `Nebula.Services/Networking/PersonService.cs`
- **Validator**: `Nebula.Services/Validators/Networking/CreatePersonCommandValidator.cs`
- **Routes**: `Nebula.API/Routes/Networking/PersonRoutes.cs`

**Use `Glob` and `Read` tools to examine these files when needed.**

### Key Rules

✅ **Do**:
- Use primary constructors: `class Foo(IBar bar)`
- Use `field ??=` for lazy initialization in properties
- Mark infrastructure as `internal sealed`
- Return `TypedResult<T>` from services
- Use `AsNoTracking()` for read queries

❌ **Don't**:
- Manually register repositories (auto-scanned)
- Throw exceptions from services (use TypedResult)
- Use AutoMapper (manual mapping only)
- Create controllers (Minimal APIs only)
- Skip validators for Create/Update operations

Whenever new features are added/modified we want to keep the following as "living documents" and update them when needed:
1. README.md
2. conventions.md
3. templates/

Add new templates when the situation permits.

### Need More Details?

See `conventions.md` for a concise 1-page reference, or read the specific template file you need.