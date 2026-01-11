using Nebula.Contracts.Repositories.DailyTasks;
using Nebula.Contracts.Repositories.Networking;

namespace Nebula.Contracts.Repositories;

/// <summary>
///     Unit of Work interface for coordinating repository operations and transactions.
/// </summary>
public interface IUnitOfWork : IDisposable
{
    /// <summary>
    ///     Gets the Person repository.
    /// </summary>
    IPersonRepository Persons { get; }

    /// <summary>
    ///     Gets the DailyTask repository.
    /// </summary>
    IDailyTaskRepository DailyTasks { get; }

    /// <summary>
    ///     Saves all changes made in this unit of work to the database.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The number of state entries written to the database.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    ///     Begins a new database transaction.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);

    /// <summary>
    ///     Commits the current transaction.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);

    /// <summary>
    ///     Rolls back the current transaction.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
}