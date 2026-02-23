namespace CourseOnline.Infrastructure.Persistence.Entities;
public class CourseSessionEntity
{
    public Guid Id { get; set; }
    public CourseEntity Course { get; set; } = null!;
    public Guid CourseId { get; set; }
    public ClassroomEntity Classroom { get; set; } = null!;
    public int ClassroomId { get; set; } 
    public string Date { get; set; } = null!;
    public string StartAt { get; set; } = null!;
    public string EndAt { get; set; } = null!;
    public byte[] RowVersion { get; set; } = null!;
    public DateTime CreatedAtUtc { get; set; }
    public DateTime ModifiedAtUtc { get; set; }

}
