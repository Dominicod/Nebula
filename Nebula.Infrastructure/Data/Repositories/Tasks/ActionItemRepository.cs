using Nebula.Contracts.Repositories.ActionItems;
using Nebula.Domain.Entities.ActionItems;

namespace Nebula.Infrastructure.Data.Repositories.Tasks;

/// <inheritdoc cref="IActionItemRepository" />
internal sealed class ActionItemRepository(NebulaDbContext context) : Repository<ActionItem, Guid>(context), IActionItemRepository;