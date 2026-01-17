using Microsoft.EntityFrameworkCore;
using Nebula.Domain.Entities.Networking;
using Task = Nebula.Domain.Entities.Tasks.Task;

namespace Nebula.Infrastructure.Data;

/// <inheritdoc />
internal sealed class NebulaDbContext : DbContext
{
    /// <inheritdoc />
    public NebulaDbContext(DbContextOptions<NebulaDbContext> options) : base(options)
    {
    }

    /// <summary>
    ///     Gets or sets the People DbSet.
    /// </summary>
    public DbSet<Person> People { get; set; }

    /// <summary>
    ///     Gets or sets the Tasks DbSet.
    /// </summary>
    public DbSet<Task> Tasks { get; set; }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(NebulaDbContext).Assembly);
    }
}