using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nebula.Domain.Entities.DailyTasks;
using Nebula.Infrastructure.Data.Entities.Configuration.Shared;

namespace Nebula.Infrastructure.Data.Entities.Configuration;

/// <summary>
///     EF Core configuration for the <see cref="DailyTask" /> entity.
/// </summary>
internal sealed class DailyTaskConfiguration : BaseEntityConfiguration<DailyTask, Guid>
{
    /// <inheritdoc />
    protected override void ConfigureEntity(EntityTypeBuilder<DailyTask> builder)
    {
        builder.ToTable("DailyTasks");

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
