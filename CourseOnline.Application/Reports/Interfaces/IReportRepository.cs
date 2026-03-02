using CourseOnline.Application.Reports.DTOs;

namespace CourseOnline.Application.Reports.Interfaces;

public interface IReportRepository
{
    Task<IReadOnlyCollection<SessionAvailabilityReport>> GetSessionAvailabilityReportAsync(CancellationToken ct);
    Task<IReadOnlyCollection<StudentUpcomingSessionsReport>> GetStudentWithUpcomingSessionsAsync(CancellationToken ct);

}
