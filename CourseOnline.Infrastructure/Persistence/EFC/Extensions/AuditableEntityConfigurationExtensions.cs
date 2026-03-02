using CourseOnline.Infrastructure.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseOnline.Infrastructure.Persistence.EFC.Extensions;

public static class AuditableEntityConfigurationExtensions
{
    public static void ConfigureAuditable<TEntity>(this EntityTypeBuilder<TEntity> builder)
        where TEntity : AuditableEntity
    {
        builder.Property(x => x.RowVersion)
            .IsRowVersion()
            .IsRequired(false); // om du kör SQLite dev

        builder.Property(x => x.CreatedAtUtc)
            .HasPrecision(0)
            .IsRequired()
            .ValueGeneratedNever(); // <-- viktigt

        builder.Property(x => x.ModifiedAtUtc)
            .HasPrecision(0)
            .IsRequired()
            .ValueGeneratedNever(); // <-- viktigt
    }
}