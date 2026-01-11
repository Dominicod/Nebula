using Nebula.Domain.Entities.Tasks;

namespace Nebula.Contracts.Repositories.Tasks;

/// <summary>
///     Repository interface for Task entity operations.
/// </summary>
public interface ITaskRepository : IRepository<Task, Guid>;
