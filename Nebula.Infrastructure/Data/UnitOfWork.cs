using Microsoft.EntityFrameworkCore.Storage;
using Nebula.Contracts.Repositories;
using Nebula.Contracts.Repositories.ActionItems;
using Nebula.Contracts.Repositories.Networking;
using Nebula.Infrastructure.Data.Repositories.Networking;
using Nebula.Infrastructure.Data.Repositories.Tasks;

namespace Nebula.Infrastructure.Data;

/// <inheritdoc />
internal sealed class UnitOfWork(NebulaDbContext context) : IUnitOfWork
{
    private readonly NebulaDbContext _context = context ?? throw new ArgumentNullException(nameof(context));
    private bool _disposed;
    private IDbContextTransaction? _transaction;

    /// <inheritdoc />
    public IPersonRepository Persons => field ??= new PersonRepository(_context);

    /// <inheritdoc />
    public IActionItemRepository ActionItems => field ??= new ActionItemRepository(_context);

    /// <inheritdoc />
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        _transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction == null)
            throw new InvalidOperationException("No active transaction to commit.");

        try
        {
            await SaveChangesAsync(cancellationToken);
            await _transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await RollbackTransactionAsync(cancellationToken);
            throw;
        }
        finally
        {
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    /// <inheritdoc />
    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction == null)
            return;

        await _transaction.RollbackAsync(cancellationToken);
        await _transaction.DisposeAsync();
        _transaction = null;
    }

    #region IDisposable Support

    public void Dispose()
    {
        Dispose(true);
    }

    private void Dispose(bool disposing)
    {
        if (_disposed)
            return;

        if (disposing)
        {
            _transaction?.Dispose();
            _context.Dispose();
        }

        _disposed = true;
    }

    #endregion
}