using CourseOnline.Infrastructure.Persistence.EFC.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseOnline.Infrastructure.Persistence.EFC.Configurations;

public sealed class CourseSessionStudentEntityConfigurations : IEntityTypeConfiguration<CourseSessionStudentEntity>
{
    public void Configure(EntityTypeBuilder<CourseSessionStudentEntity> builder)
    {
        builder.ToTable("CourseSessionStudents");

        builder.HasKey(x => new { x.StudentId, x.CourseSessionId });

        builder.HasOne(x => x.Student)
            .WithMany(s => s.CourseSessionStudents) 
            .HasForeignKey(x => x.StudentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.CourseSession)
            .WithMany(cs => cs.CourseSessionStudents)
            .HasForeignKey(x => x.CourseSessionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.CourseSessionId);
        builder.HasIndex(x => x.StudentId);
    }
}
