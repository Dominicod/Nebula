using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nebula.Domain.Entities.ActionItems;
using Nebula.Infrastructure.Data.Entities.Configuration.Shared;

namespace Nebula.Infrastructure.Data.Entities.Configuration;

/// <summary>
///     EF Core configuration for the <see cref="ActionItem" /> entity.
/// </summary>
internal sealed class TaskConfiguration : BaseEntityConfiguration<ActionItem, Guid>
{
    /// <inheritdoc />
    protected override void ConfigureEntity(EntityTypeBuilder<ActionItem> builder)
    {
        builder.ToTable("ActionItems");

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