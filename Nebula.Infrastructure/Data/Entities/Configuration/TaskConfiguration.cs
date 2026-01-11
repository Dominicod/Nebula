using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nebula.Infrastructure.Data.Entities.Configuration.Shared;

namespace Nebula.Infrastructure.Data.Entities.Configuration;

/// <summary>
///     EF Core configuration for the <see cref="Domain.Entities.Tasks.Task" /> entity.
/// </summary>
internal sealed class TaskConfiguration : BaseEntityConfiguration<Domain.Entities.Tasks.Task, Guid>
{
    /// <inheritdoc />
    protected override void ConfigureEntity(EntityTypeBuilder<Domain.Entities.Tasks.Task> builder)
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
