using CourseOnline.Domain.Models;

namespace CourseOnline.Application.Registrations.Interfaces;
public interface IRegistrationRepository
{
    Task<bool> ExistsAsync(Guid studentId, Guid courseSessionId, CancellationToken ct);

    Task<CourseSessionStudent> AddAsync(CourseSessionStudent registration, CancellationToken ct);

    Task<bool> RemoveAsync(Guid studentId, Guid courseSessionId, CancellationToken ct);

    Task<IReadOnlyCollection<CourseSessionStudent>> GetBySessionIdAsync(Guid courseSessionId, CancellationToken ct);

    Task<IReadOnlyCollection<CourseSessionStudent>> GetByStudentIdAsync(Guid studentId, CancellationToken ct);

    Task<int> CountBySessionIdAsync(Guid courseSessionId, CancellationToken ct);
}
