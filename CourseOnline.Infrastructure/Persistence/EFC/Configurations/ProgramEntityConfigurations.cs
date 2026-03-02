    using CourseOnline.Infrastructure.Persistence.EFC.Entities;
    using CourseOnline.Infrastructure.Persistence.EFC.Extensions;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    namespace CourseOnline.Infrastructure.Persistence.EFC.Configurations;

    public sealed class ProgramEntityConfigurations : IEntityTypeConfiguration<ProgramEntity>
    {
        public void Configure(EntityTypeBuilder<ProgramEntity> builder)
        {
            builder.ToTable("Programs");

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

            builder.ConfigureAuditable();

            builder.HasIndex(x => x.Name)
                .IsUnique();

        }
    }
