using CourseOnline.Domain.Exceptions;

namespace CourseOnline.Domain.Models;
public sealed class Course
{
    public Guid Id { get; }
    public string Name { get; private set; } = null!;
    public int? DurationWeeks { get; private set; }
    public int? MaxStudents { get; private set; }
    public Guid? TeacherId { get; private set; }
    public Guid? ProgramId { get; private set; }

    public Course(Guid id, string name, int? durationWeeks, int? maxStudents, Guid? teacherId, Guid? programId)
    {
        if (id == Guid.Empty) throw new DomainValidationException("Id is required");
        if (string.IsNullOrWhiteSpace(name)) throw new DomainValidationException("Program name is required");
        if (durationWeeks is not null && durationWeeks <= 0) throw new DomainValidationException("DurationWeeks must be greater than 0");
        if (maxStudents is not null && maxStudents <= 0) throw new DomainValidationException("MaxStudents must be greater than 0");


        Id = id;
        Name = name.Trim().ToLower();
        DurationWeeks = durationWeeks;
        MaxStudents = maxStudents;
        TeacherId = teacherId;
        ProgramId = programId;
    }

    public void Update(
        string name,
        int? durationWeeks,
        int? maxStudents,
        Guid? teacherId,
        Guid? programId)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainValidationException("Course name is required");

        if (durationWeeks is not null && durationWeeks <= 0)
            throw new DomainValidationException("DurationWeeks must be greater than 0");

        if (maxStudents is not null && maxStudents <= 0)
            throw new DomainValidationException("MaxStudents must be greater than 0");

        Name = name.Trim().ToLower();
        DurationWeeks = durationWeeks;
        MaxStudents = maxStudents;
        TeacherId = teacherId;
        ProgramId = programId;
    }

    public void AssignTeacher(Guid? teacherId)
    {
        TeacherId = teacherId;
    }

    public void AssignProgram(Guid? programId)
    {
        ProgramId = programId;
    }
}
