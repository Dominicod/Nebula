using Task = Nebula.Domain.Entities.Tasks.Task;

namespace Nebula.Contracts.Repositories.Tasks;

/// <summary>
///     Repository interface for Task entity operations.
/// </summary>
public interface ITaskRepository : IRepository<Task, Guid>;