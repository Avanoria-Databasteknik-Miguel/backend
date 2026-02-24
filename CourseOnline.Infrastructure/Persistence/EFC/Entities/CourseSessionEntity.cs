namespace CourseOnline.Infrastructure.Persistence.EFC.Entities;
public class CourseSessionEntity
{
    public Guid Id { get; set; }
    public CourseEntity Course { get; set; } = null!;
    public Guid CourseId { get; set; }
    public ClassroomEntity Classroom { get; set; } = null!;
    public int ClassroomId { get; set; }
    public DateTime StartDateTimeUtc { get; set; }
    public DateTime EndDateTimeUtc { get; set; }
    public byte[] RowVersion { get; set; } = null!;
    public DateTime CreatedAtUtc { get; set; }
    public DateTime ModifiedAtUtc { get; set; }

}
