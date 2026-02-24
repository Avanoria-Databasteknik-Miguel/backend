using CourseOnline.Infrastructure.Persistence.EFC.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseOnline.Infrastructure.Persistence.EFC.Configurations;

public sealed class ProgramEntityConfigurations : IEntityTypeConfiguration<ProgramEntity>
{
    public void Configure(EntityTypeBuilder<ProgramEntity> builder)
    {
        builder.ToTable("Programs", t =>
        {
            t.HasCheckConstraint(
                "CK_Programs_DurationWeeks_Positive",
                "[DurationWeeks] IS NULL OR [DurationWeeks] > 0"
            );

            t.HasCheckConstraint(
                "CK_Programs_MaxStudents_Positive",
                "[MaxStudents] IS NULL OR [MaxStudents] > 0"
            );

            t.HasCheckConstraint(
                "CK_Programs_Name_NotEmpty",
                "LEN(LTRIM(RTRIM([Name]))) > 0"
            );
        });

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.DurationWeeks)
            .IsRequired(false);

        builder.Property(x => x.MaxStudents)
            .IsRequired(false);

        builder.Property(x => x.RowVersion)
            .IsRowVersion()
            .IsRequired();

        builder.Property(x => x.CreatedAtUtc)
            .HasPrecision(0)
            .HasDefaultValueSql("SYSUTCDATETIME()")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.ModifiedAtUtc)
            .HasPrecision(0)
            .HasDefaultValueSql("SYSUTCDATETIME()")
            .ValueGeneratedOnAddOrUpdate();

        builder.HasIndex(x => x.Name)
            .IsUnique();

    }
}
