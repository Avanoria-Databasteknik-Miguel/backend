using CourseOnline.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace CourseOnline.Infrastructure.Persistence;

public sealed class CourseOnlineDbContext(DbContextOptions<CourseOnlineDbContext> options) : DbContext(options)
{
    public DbSet<CategoryEntity> Categories => Set<CategoryEntity>();
    public DbSet<ClassroomEntity> Classrooms => Set<ClassroomEntity>();
    public DbSet<CourseCategoryEntity> CourseCategories => Set<CourseCategoryEntity>();
    public DbSet<CourseEntity> Courses => Set<CourseEntity>();
    public DbSet<CourseSessionEntity> CourseSessions => Set<CourseSessionEntity>();
    public DbSet<CourseSessionStudentEntity> CourseSessionStudents => Set<CourseSessionStudentEntity>();
    public DbSet<FloorEntity> Floors => Set<FloorEntity>();
    public DbSet<ProgramEntity> Programs => Set<ProgramEntity>();
    public DbSet<SchoolEntity> Schools => Set<SchoolEntity>();
    public DbSet<StudentCourseEntity> StudentCourses => Set<StudentCourseEntity>();
    public DbSet<StudentEntity> Students => Set<StudentEntity>();
    public DbSet<TeacherEntity> Teachers => Set<TeacherEntity>();



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProgramEntity>(entity =>
        {
            entity.ToTable("Programs");

            entity.HasKey(e => e.Id).HasName("PK_Programs_Id");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("NEWSEQUENTIALID()", "DF_Programs_Id");

            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(e => e.DurationWeeks);

            entity.Property(e => e.MaxStudents);

            entity.Property(e => e.RowVersion)
                .IsRowVersion()
                .IsRequired();

            entity.Property(e => e.CreatedAtUtc)
                .HasPrecision(0)
                .HasDefaultValueSql("SYSUTCDATETIME()", "DF_Programs_CreatedAtUtc")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.ModifiedAtUtc)
                .HasPrecision(0)
                .HasDefaultValueSql("SYSUTCDATETIME()", "DF_Programs_ModifiedAtUtc")
                .ValueGeneratedOnAddOrUpdate();

            entity.HasIndex(e => e.Name, "UQ_Programs_Name").IsUnique();
            entity.ToTable(tb => tb.HasCheckConstraint("CK_Programs_Name_NotEmpty", "LEN(LTRIM(RTRIM([Name]))) > 0"));
        
        });
    }
}
