namespace CourseOnline.Infrastructure.Persistence.Entities;
public class SchoolEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string StreetName { get; set; } = null!;
    public string HouseNumber { get; set; } = null!;
    public string ZipCode { get; set; } = null!;
    public string PostalTown { get; set; } = null!;
    public byte[] RowVersion { get; set; } = null!;
    public DateTime CreatedAtUtc { get; set; }
    public DateTime ModifiedAtUtc { get; set; }
}
