namespace CourseOnline.Infrastructure.Persistence.Entities;
public class ProgramEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public int? DurationWeeks { get; set; }
    public int? MaxStudents { get; set; }
    public byte[] RowVersion { get; set; } = null!;
    public DateTime CreatedAtUtc { get; set; }
    public DateTime ModifiedAtUtc { get; set; }

}