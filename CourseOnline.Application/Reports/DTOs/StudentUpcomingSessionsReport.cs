namespace CourseOnline.Application.Reports.DTOs;

public sealed record StudentUpcomingSessionsReport(
    Guid StudentId,
    string FirstName,
    string LastName,
    string Email,
    string? ProgramName,
    string CourseName,
    DateTime StartDateTimeUtc,
    string ClassroomName,
    string FloorLevel);
