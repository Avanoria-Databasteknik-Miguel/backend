using CourseOnline.Domain.Exceptions;

namespace CourseOnline.Domain.Models;

public sealed class CourseSession
{
    public Guid Id { get; }
    public Guid CourseId { get; private set; }
    public int ClassroomId { get; private set; }
    public DateTime StartDateTimeUtc { get; private set; }
    public DateTime EndDateTimeUtc { get; private set; }

    public CourseSession(Guid id, Guid courseId, int classroomId, DateTime startDateTimeUtc, DateTime endDateTimeUtc)
    {
        if (id == Guid.Empty) throw new DomainValidationException("Id is required");
        if (courseId == Guid.Empty) throw new DomainValidationException("CourseId is required");
        if (classroomId <= 0) throw new DomainValidationException("ClassroomId is required");
        if (startDateTimeUtc >= endDateTimeUtc) throw new DomainValidationException("Start must be before End");

        Id = id;
        CourseId = courseId;
        ClassroomId = classroomId;
        StartDateTimeUtc = startDateTimeUtc;
        EndDateTimeUtc = endDateTimeUtc;
    }

    public void Update(DateTime startDateTimeUtc, DateTime endDateTimeUtc)
    {
        if (startDateTimeUtc >= endDateTimeUtc) throw new DomainValidationException("Start must be before End");

        StartDateTimeUtc = startDateTimeUtc;
        EndDateTimeUtc = endDateTimeUtc;
    }
}