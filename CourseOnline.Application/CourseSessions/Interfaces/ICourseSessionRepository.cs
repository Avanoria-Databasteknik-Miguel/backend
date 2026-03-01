using CourseOnline.Application.Common.Interfaces;
using CourseOnline.Application.Common.Results;
using CourseOnline.Domain.Models;

namespace CourseOnline.Application.CourseSessions.Interfaces;
public interface ICourseSessionRepository : IRepositoryBase<CourseSession, Guid>
{
    Task<Result<CourseSession>> GetByCourseIdAsync(Guid courseId, CancellationToken ct);
}
