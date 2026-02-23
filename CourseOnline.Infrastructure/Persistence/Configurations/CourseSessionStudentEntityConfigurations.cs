using CourseOnline.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseOnline.Infrastructure.Persistence.Configurations;

public sealed class CourseSessionStudentEntityConfigurations : IEntityTypeConfiguration<CourseSessionStudentEntity>
{
    public void Configure(EntityTypeBuilder<CourseSessionStudentEntity> builder)
    {
        builder.ToTable("CourseSessionStudents");

        builder.HasKey(x => new { x.StudentId, x.CourseSessionId });

        builder.HasOne(x => x.Student)
            .WithMany() // eller .WithMany(s => s.CourseSessionStudents) om du lägger collection
            .HasForeignKey(x => x.StudentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.CourseSession)
            .WithMany() // eller .WithMany(cs => cs.CourseSessionStudents)
            .HasForeignKey(x => x.CourseSessionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.CourseSessionId);
    }
}
