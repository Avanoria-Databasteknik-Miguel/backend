namespace CourseOnline.Infrastructure.Persistence.Entities;

public class CourseEntity
{
    public Guid Id { get; set; }
    public TeacherEntity? PrimaryTeacher { get; set; } = null!;
    public TeacherEntity? SubstituteTeacher { get; set; } = null!;

    public Guid? PrimaryTeacherId { get; set; }
    public ProgramEntity? Program { get; set; }
    public Guid? ProgramId { get; set; }
    public string Name { get; set; } = null!;
    public int DurationWeekd { get; set; }
}
