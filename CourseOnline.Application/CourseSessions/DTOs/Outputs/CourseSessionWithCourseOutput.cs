using CourseOnline.Domain.Models;

namespace CourseOnline.Application.CourseSessions.DTOs.Outputs;
public sealed record CourseSessionWithCourseOutput(Guid Id, Guid CourseId, int ClassroomId, DateTime StartDateTimeUtc, DateTime EndDateTimeUtc, Course Course)
{
}
