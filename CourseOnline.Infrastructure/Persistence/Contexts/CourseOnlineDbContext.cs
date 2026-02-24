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
        => modelBuilder.ApplyConfigurationsFromAssembly(typeof(CourseOnlineDbContext).Assembly);
    
}
