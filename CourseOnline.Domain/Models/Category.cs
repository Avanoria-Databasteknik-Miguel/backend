using CourseOnline.Domain.Exceptions;

namespace CourseOnline.Domain.Models;
public sealed class Category
{
    public Guid Id { get; }
    public string Name { get; private set; }

    public Category(Guid id, string name)
    {
        if (id == Guid.Empty) throw new DomainValidationException("Id is required");
        if (string.IsNullOrWhiteSpace(name)) throw new DomainValidationException("Name is required");

        Id = id;
        Name = name.Trim().ToLower();
    }

    public void Update(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new DomainValidationException("Name is required");

        Name = name.Trim().ToLower();
    }

}
