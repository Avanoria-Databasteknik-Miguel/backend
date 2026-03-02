using CourseOnline.Application.Reports.DTOs;

namespace CourseOnline.Application.Reports.Interfaces;
public interface IReportService
{
    Task<IReadOnlyCollection<SessionAvailabilityReport>> GetSessionAvailabilityAsync(CancellationToken ct);
    Task<IReadOnlyCollection<StudentUpcomingSessionsReport>> GetStudentWithUpcomingSessionsAsync(CancellationToken ct);
}
