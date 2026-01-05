// Template: Repository Interface and Implementation
// Interface Location: Contracts/Nebula.Contracts.Repositories/{Domain}/I{EntityName}Repository.cs
// Implementation Location: Nebula.Infrastructure/Data/Repositories/{Domain}/{EntityName}Repository.cs

// ============================================================================
// INTERFACE (in Nebula.Contracts.Repositories)
// ============================================================================

using Nebula.Domain.Entities.{Domain};

namespace Nebula.Contracts.Repositories.{Domain};

/// <summary>
///     Repository interface for {EntityName} entities.
/// </summary>
public interface I{EntityName}Repository : IRepository<{EntityName}, Guid>
{
    // Add entity-specific query methods if needed
    // Task<IEnumerable<{EntityName}>> FindByPropertyAsync(string property, CancellationToken cancellationToken = default);
}

// ============================================================================
// IMPLEMENTATION (in Nebula.Infrastructure/Data/Repositories)
// ============================================================================

using Nebula.Contracts.Repositories.{Domain};
using Nebula.Domain.Entities.{Domain};
using Nebula.Infrastructure.Data.Context;

namespace Nebula.Infrastructure.Data.Repositories.{Domain};

/// <summary>
///     Repository implementation for {EntityName} entities.
/// </summary>
internal sealed class {EntityName}Repository(NebulaDbContext context)
    : Repository<{EntityName}, Guid>(context), I{EntityName}Repository
{
    // Most repositories can be empty - base class handles CRUD

    // Add custom implementations only if needed:
    // public async Task<IEnumerable<{EntityName}>> FindByPropertyAsync(string property, CancellationToken cancellationToken = default)
    // {
    //     return await DbSet
    //         .AsNoTracking()
    //         .Where(e => e.Property == property)
    //         .ToListAsync(cancellationToken);
    // }
}

// Notes:
// - Interface: Extend IRepository<TEntity, TId>
// - Implementation: Mark 'internal sealed', extend Repository<TEntity, TId>
// - Use primary constructor with NebulaDbContext
// - Auto-registered via assembly scanning (no DI registration needed)
// - Most repositories are one-liners - only add methods for complex queries
