using CourseOnline.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseOnline.Infrastructure.Persistence.Configurations;

public sealed class ClassroomEntityConfigurations : IEntityTypeConfiguration<ClassroomEntity>
{
    public void Configure(EntityTypeBuilder<ClassroomEntity> builder)
    {
        builder.ToTable("Classrooms");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Seats)
            .IsRequired();

        builder.HasIndex(x => x.Name)
            .IsUnique();

        builder.Property(x => x.FloorId);

        builder.HasOne(x => x.Floor)
            .WithMany(x => x.Classrooms) 
            .HasForeignKey(x => x.FloorId)
            .OnDelete(DeleteBehavior.Restrict);

    }

}
