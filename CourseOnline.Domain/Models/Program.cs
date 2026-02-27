using CourseOnline.Domain.Exceptions;

namespace CourseOnline.Domain.Models;
public sealed class Program
{
    public Guid Id { get; }
    public string Name { get; private set; } = null!;
    public int? DurationWeeks { get; private set; }
    public int? MaxStudents { get; private set; }

    public Program(Guid id, string name, int? durationWeeks, int? maxStudents)
    {
        if (id == Guid.Empty) throw new DomainValidationException("Id is required");
        if (string.IsNullOrWhiteSpace(name)) throw new DomainValidationException("Program name is required");
        if (durationWeeks is not null && durationWeeks <= 0) throw new DomainValidationException("DurationWeeks must be greater than 0");
        if (maxStudents is not null && maxStudents <= 0) throw new DomainValidationException("MaxStudents must be greater than 0");

        Id = id;
        Name = name.Trim();
        DurationWeeks = durationWeeks;
        MaxStudents = maxStudents;
    }

    public void Update(string name, int? durationWeeks, int? maxStudents)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new DomainValidationException("Program name is required");
        if (durationWeeks is not null && durationWeeks <= 0) throw new DomainValidationException("DurationWeeks must be greater than 0");
        if (maxStudents is not null && maxStudents <= 0) throw new DomainValidationException("MaxStudents must be greater than 0");

        Name = name.Trim();
        DurationWeeks = durationWeeks;
        MaxStudents = maxStudents;
    }
}
