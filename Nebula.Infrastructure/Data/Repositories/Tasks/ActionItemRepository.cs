using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Nebula.Contracts.Repositories.ActionItems;
using Nebula.Domain.Entities.ActionItems;

namespace Nebula.Infrastructure.Data.Repositories.Tasks;

/// <inheritdoc cref="IActionItemRepository" />
internal sealed class ActionItemRepository(NebulaDbContext context) : Repository<ActionItem, Guid>(context), IActionItemRepository
{
    private readonly NebulaDbContext _context = context;

    /// <inheritdoc />
    public override async Task<ActionItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Tasks
            .Include(a => a.ActionItemType)
            .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
    }

    /// <inheritdoc />
    public override async Task<IEnumerable<ActionItem>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Tasks
            .Include(a => a.ActionItemType)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public override async Task<IEnumerable<ActionItem>> FindAsync(Expression<Func<ActionItem, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return await _context.Tasks
            .Include(a => a.ActionItemType)
            .AsNoTracking()
            .Where(predicate)
            .ToListAsync(cancellationToken);
    }
}