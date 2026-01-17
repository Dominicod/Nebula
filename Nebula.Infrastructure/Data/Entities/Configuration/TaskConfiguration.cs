using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nebula.Infrastructure.Data.Entities.Configuration.Shared;
using Task = Nebula.Domain.Entities.Tasks.Task;

namespace Nebula.Infrastructure.Data.Entities.Configuration;

/// <summary>
///     EF Core configuration for the <see cref="Domain.Entities.Tasks.Task" /> entity.
/// </summary>
internal sealed class TaskConfiguration : BaseEntityConfiguration<Task, Guid>
{
    /// <inheritdoc />
    protected override void ConfigureEntity(EntityTypeBuilder<Task> builder)
    {
        builder.ToTable("Tasks");

        builder.Property(t => t.Text)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Property(t => t.IsCompleted)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(t => t.CompletedAt)
            .IsRequired(false);
    }
}