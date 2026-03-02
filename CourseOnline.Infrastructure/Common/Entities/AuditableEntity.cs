namespace CourseOnline.Infrastructure.Common.Entities;
public abstract class AuditableEntity
{
    public byte[]? RowVersion { get; set; } = default!;
    public DateTime CreatedAtUtc { get; set; }
    public DateTime ModifiedAtUtc { get; set; }
}
