using Nebula.Domain.Entities.DailyTasks;

namespace Nebula.Contracts.Repositories.DailyTasks;

/// <summary>
///     Repository interface for DailyTask entity operations.
/// </summary>
public interface IDailyTaskRepository : IRepository<DailyTask, Guid>;
