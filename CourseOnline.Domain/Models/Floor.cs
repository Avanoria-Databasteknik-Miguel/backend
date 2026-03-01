using CourseOnline.Domain.Exceptions;

namespace CourseOnline.Domain.Models;
public sealed class Floor
{
    public int Id { get; }
    public string Level { get; private set; }

    public Floor(int id, string level)
    {
        if (id < 0) throw new DomainValidationException("Id is required");
        if (string.IsNullOrWhiteSpace(level)) throw new DomainValidationException("Level is required");

        Id = id;
        Level = level.Trim().ToLower();
    }

    public void Update(string level)
    {
        if (string.IsNullOrWhiteSpace(level)) throw new DomainValidationException("Level is required");

        Level = level.Trim().ToLower();
    }

}
