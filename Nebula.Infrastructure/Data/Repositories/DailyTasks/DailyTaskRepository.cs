using Nebula.Contracts.Repositories.DailyTasks;
using Nebula.Domain.Entities.DailyTasks;

namespace Nebula.Infrastructure.Data.Repositories.DailyTasks;

/// <inheritdoc cref="IDailyTaskRepository" />
internal sealed class DailyTaskRepository(NebulaDbContext context) : Repository<DailyTask, Guid>(context), IDailyTaskRepository;
