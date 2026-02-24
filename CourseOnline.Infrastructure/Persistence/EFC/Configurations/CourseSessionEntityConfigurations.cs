using CourseOnline.Infrastructure.Persistence.EFC.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseOnline.Infrastructure.Persistence.EFC.Configurations;

public sealed class CourseSessionEntityConfigurations : IEntityTypeConfiguration<CourseSessionEntity>
{
    public void Configure(EntityTypeBuilder<CourseSessionEntity> builder)
    {
        builder.ToTable("CourseSessions", t =>
        {
            t.HasCheckConstraint(
                "CK_CourseSessions_StartBeforeEnd",
                "[StartDateTimeUtc] < [EndDateTimeUtc]"
            );
        });

        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Course)
            .WithMany(c => c.CourseSessions)
            .HasForeignKey(x => x.CourseId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Classroom)
            .WithMany(c => c.CourseSessions) 
            .HasForeignKey(x => x.ClassroomId)
            .OnDelete(DeleteBehavior.Restrict);


        builder.Property(x => x.StartDateTimeUtc)
            .IsRequired()
            .HasPrecision(0);

        builder.Property(x => x.EndDateTimeUtc)
            .IsRequired()
            .HasPrecision(0);

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


        builder.HasIndex(x => x.CourseId);
        builder.HasIndex(x => x.ClassroomId);
    }
}
