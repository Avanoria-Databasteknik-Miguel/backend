namespace CourseOnline.Application.Reports.DTOs;

public sealed record SessionAvailabilityReport(
    Guid CourseSessionId,
    string CourseName,
    DateTime StartDateTimeUtc,
    DateTime EndDateTimeUtc,
    string ClassroomName,
    string FloorLevel,
    int Seats,
    int RegisteredStudents,
    int AvailableSeats);
