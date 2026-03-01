namespace CourseOnline.Application.CourseSessions.DTOs.Inputs;

public sealed record UpdateCourseSessionInput(Guid Id, Guid CourseId, int ClassroomId, DateTime StartDateTimeUtc, DateTime EndDateTimeUtc);
