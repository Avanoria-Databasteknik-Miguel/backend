
using CourseOnline.Application.Common.Results;
using CourseOnline.Domain.Models;

namespace CourseOnline.Application.Contracts.Registrations;
public interface IRegistrationService
{
    Task<Result<CourseSessionStudent>> RegisterStudentAsync(Guid courseSessionId, Guid studentId, CancellationToken ct);

    Task<Result> UnregisterStudentAsync(Guid courseSessionId, Guid studentId, CancellationToken ct);

    Task<Result<IReadOnlyCollection<CourseSessionStudent>>> GetRegistrationsBySessionIdAsync(Guid courseSessionId, CancellationToken ct);

    Task<Result<IReadOnlyCollection<CourseSessionStudent>>> GetRegistrationsByStudentIdAsync(Guid studentId, CancellationToken ct);

    Task<Result<int>> CountRegistrationsBySessionIdAsync(Guid courseSessionId, CancellationToken ct);
}
