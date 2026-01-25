using Nebula.Contracts.Repositories.ActionItemTypes;
using Nebula.Domain.Entities.ActionItemTypes;

namespace Nebula.Infrastructure.Data.Repositories.ActionItemTypes;

/// <inheritdoc cref="IActionItemTypeRepository" />
internal sealed class ActionItemTypeRepository(NebulaDbContext context) : Repository<ActionItemType, Guid>(context), IActionItemTypeRepository;
