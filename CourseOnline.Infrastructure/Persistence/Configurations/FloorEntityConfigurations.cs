
using CourseOnline.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseOnline.Infrastructure.Persistence.Configurations;

public sealed class FloorEntityConfigurations : IEntityTypeConfiguration<FloorEntity>
{
    public void Configure(EntityTypeBuilder<FloorEntity> builder)
    {
        builder.ToTable("Floors");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Level)
            .HasMaxLength(10)
            .IsRequired();

        builder.HasIndex(x => x.Level)
            .IsUnique();

        builder.HasMany(x => x.Classrooms)
            .WithOne(x => x.Floor)
            .HasForeignKey(x => x.FloorId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}