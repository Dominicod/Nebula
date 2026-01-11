using Nebula.Contracts.Repositories.Tasks;
using Nebula.Domain.Entities.Tasks;

namespace Nebula.Infrastructure.Data.Repositories.Tasks;

/// <inheritdoc cref="ITaskRepository" />
internal sealed class TaskRepository(NebulaDbContext context) : Repository<Task, Guid>(context), ITaskRepository;
