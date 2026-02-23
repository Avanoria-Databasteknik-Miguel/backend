namespace CourseOnline.Infrastructure.Persistence.Entities;
public class StudentEntity
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public ProgramEntity? Program { get; set; } 
    public Guid ProgramId { get; set; }
    public ICollection<StudentCourseEntity> StudentCourses { get; set; } = [];
    public string? ImageUrl { get; set; }
    public byte[] RowVersion { get; set; } = null!;
    public DateTime CreatedAtUtc { get; set; }
    public DateTime ModifiedAtUtc { get; set; }
}
