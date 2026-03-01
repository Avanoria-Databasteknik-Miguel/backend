using CourseOnline.Domain.Exceptions;

namespace CourseOnline.Domain.Models;
public class CourseSessionStudent
{
    public Guid StudentId { get; }
    public Guid CourseSessionId { get; }

    public CourseSessionStudent(Guid studentId, Guid courseSessionId)
    {
        if (studentId == Guid.Empty)
            throw new DomainValidationException("StudentId is required.");

        if (courseSessionId == Guid.Empty)
            throw new DomainValidationException("CourseSessionId is required.");

        StudentId = studentId;
        CourseSessionId = courseSessionId;
    }
}
