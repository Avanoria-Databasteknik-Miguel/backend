using CourseOnline.Infrastructure.Persistence.EFC.Entities;
using Microsoft.EntityFrameworkCore;

namespace CourseOnline.Infrastructure.Persistence.Contexts;

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
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CourseOnlineDbContext).Assembly);
        if (Database.IsSqlServer())
        {
            modelBuilder.Entity<ProgramEntity>()
                .ToTable("Programs", t => t.HasCheckConstraint(
                    "CK_Programs_Name_NotEmpty",
                    "LEN(LTRIM(RTRIM([Name]))) > 0"
                ));

            modelBuilder.Entity<StudentEntity>()
                .ToTable("Students", t => t.HasCheckConstraint(
                    "CK_Students_Email_NotEmpty",
                    "LEN(LTRIM(RTRIM([Email]))) > 0"
                ));

            modelBuilder.Entity<TeacherEntity>()
                .ToTable("Teachers", t => t.HasCheckConstraint(
                    "CK_Teachers_Email_NotEmpty",
                    "LEN(LTRIM(RTRIM([Email]))) > 0"
                ));

            modelBuilder.Entity<SchoolEntity>()
                .ToTable("Schools", t =>
                {
                    t.HasCheckConstraint("CK_Schools_Name_NotEmpty", "LEN(LTRIM(RTRIM([Name]))) > 0");
                    t.HasCheckConstraint("CK_Schools_StreetName_NotEmpty", "LEN(LTRIM(RTRIM([StreetName]))) > 0");
                });
        }
        else if (Database.IsSqlite())
        {
            modelBuilder.Entity<ProgramEntity>()
                .ToTable("Programs", t => t.HasCheckConstraint(
                    "CK_Programs_Name_NotEmpty",
                    "length(trim(Name)) > 0"
                ));

            modelBuilder.Entity<StudentEntity>()
                .ToTable("Students", t => t.HasCheckConstraint(
                    "CK_Students_Email_NotEmpty",
                    "length(trim(Email)) > 0"
                ));

            modelBuilder.Entity<TeacherEntity>()
                .ToTable("Teachers", t => t.HasCheckConstraint(
                    "CK_Teachers_Email_NotEmpty",
                    "length(trim(Email)) > 0"
                ));

            modelBuilder.Entity<SchoolEntity>()
                .ToTable("Schools", t =>
                {
                    t.HasCheckConstraint("CK_Schools_Name_NotEmpty", "length(trim(Name)) > 0");
                    t.HasCheckConstraint("CK_Schools_StreetName_NotEmpty", "length(trim(StreetName)) > 0");
                });
        }
    }
    
}
