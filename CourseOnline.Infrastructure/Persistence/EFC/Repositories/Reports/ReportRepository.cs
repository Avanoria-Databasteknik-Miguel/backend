using CourseOnline.Application.Reports.DTOs;
using CourseOnline.Application.Reports.Interfaces;
using CourseOnline.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CourseOnline.Infrastructure.Persistence.EFC.Repositories.Reports;

public sealed class ReportRepository(CourseOnlineDbContext context) : IReportRepository
{

    private readonly CourseOnlineDbContext _context = context;

    public async Task<IReadOnlyCollection<SessionAvailabilityReport>> GetSessionAvailabilityReportAsync(CancellationToken ct)
    {
        return await _context.CourseSessions
            .AsNoTracking()
            .Select(session => new SessionAvailabilityReport(
                session.Id,
                session.Course.Name,
                session.StartDateTimeUtc,
                session.EndDateTimeUtc,
                session.Classroom.Name,
                session.Classroom.Floor.Level,
                session.Classroom.Seats,
                session.CourseSessionStudents.Count,
                session.Classroom.Seats - session.CourseSessionStudents.Count
            ))
            .OrderBy(x => x.StartDateTimeUtc)
            .ToListAsync(ct);
    }

    public async Task<IReadOnlyCollection<StudentUpcomingSessionsReport>> GetStudentWithUpcomingSessionsAsync(CancellationToken ct)
    {
        var now = DateTime.UtcNow;

        return await _context.CourseSessionStudents
            .AsNoTracking()
            .Where(x => x.CourseSession.StartDateTimeUtc > now)
            .Select(x => new StudentUpcomingSessionsReport(
                x.Student.Id,
                x.Student.FirstName,
                x.Student.LastName,
                x.Student.Email,
                x.Student.Program != null ? x.Student.Program.Name : null,
                x.CourseSession.Course.Name,
                x.CourseSession.StartDateTimeUtc,
                x.CourseSession.Classroom.Name,
                x.CourseSession.Classroom.Floor.Level
            ))
            .OrderBy(x => x.LastName)
            .ThenBy(x => x.StartDateTimeUtc)
            .ToListAsync(ct);
    }
}
