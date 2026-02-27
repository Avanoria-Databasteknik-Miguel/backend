namespace CourseOnline.Infrastructure.Common.Entities;
public abstract class AuditableEntity
{
    public byte[] RowVersion { get; protected set; } = default!;
    public DateTime CreatedAtUtc { get; protected set; }
    public DateTime ModifiedAtUtc { get; protected set; }
}
