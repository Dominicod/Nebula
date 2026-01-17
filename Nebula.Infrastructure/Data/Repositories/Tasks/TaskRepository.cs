using Nebula.Contracts.Repositories.Tasks;
using Task = Nebula.Domain.Entities.Tasks.Task;

namespace Nebula.Infrastructure.Data.Repositories.Tasks;

/// <inheritdoc cref="ITaskRepository" />
internal sealed class TaskRepository(NebulaDbContext context) : Repository<Task, Guid>(context), ITaskRepository;