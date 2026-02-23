namespace CourseOnline.Infrastructure.Persistence.Entities;

public class CourseEntity
{
    public Guid Id { get; set; }
    public TeacherEntity? Teacher { get; set; }
    public Guid? TeacherId { get; set; }
    public ICollection<CourseCategoryEntity> CourseCategories { get; set; } = [];
    public ProgramEntity? Program { get; set; }
    public Guid? ProgramId { get; set; }
    public string Name { get; set; } = null!;
    public int? DurationWeeks { get; set; }
    public int? MaxStudents { get; set; }
    public ICollection<CourseSessionEntity> CourseSessions { get; set; } = [];
    public ICollection<StudentCourseEntity> StudentCourses { get; set; } = [];
}
