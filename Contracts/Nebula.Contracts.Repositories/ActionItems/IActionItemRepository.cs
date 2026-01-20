using Nebula.Domain.Entities.ActionItems;

namespace Nebula.Contracts.Repositories.ActionItems;

/// <summary>
///     Repository interface for ActionItem entity operations.
/// </summary>
public interface IActionItemRepository : IRepository<ActionItem, Guid>;