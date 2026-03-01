namespace CourseOnline.Application.CourseSessions.DTOs.Inputs;

public sealed record CreateCourseSessionInput(Guid CourseId, int ClassroomId, DateTime StartDateTimeUtc, DateTime EndDateTimeUtc);
