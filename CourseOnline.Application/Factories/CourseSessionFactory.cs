using CourseOnline.Application.CourseSessions.DTOs.Inputs;
using CourseOnline.Domain.Models;

namespace CourseOnline.Application.Factories;
public static class CourseSessionFactory
{
    public static CourseSession Create(CreateCourseSessionInput input) => new(Guid.NewGuid(), input.CourseId, input.ClassroomId, input.StartDateTimeUtc, input.EndDateTimeUtc);
}
