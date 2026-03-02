using CourseOnline.Application.Reports.DTOs;
using CourseOnline.Application.Reports.Interfaces;

namespace CourseOnline.Application.Services;

public sealed class ReportService(IReportRepository repo) : IReportService
{
    private readonly IReportRepository _repo = repo;

    public Task<IReadOnlyCollection<SessionAvailabilityReport>> GetSessionAvailabilityAsync(CancellationToken ct) => _repo.GetSessionAvailabilityReportAsync(ct);

    public Task<IReadOnlyCollection<StudentUpcomingSessionsReport>> GetStudentWithUpcomingSessionsAsync(CancellationToken ct) => _repo.GetStudentWithUpcomingSessionsAsync(ct);
    
}
