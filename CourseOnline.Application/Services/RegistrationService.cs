using CourseOnline.Application.Common.Interfaces;
using CourseOnline.Application.Common.Results;
using CourseOnline.Application.Contracts.Registrations;
using CourseOnline.Application.CourseSessions.Interfaces;
using CourseOnline.Application.Registrations.Interfaces;
using CourseOnline.Application.Students.Interfaces;
using CourseOnline.Domain.Models;

namespace CourseOnline.Application.Services;

public sealed class RegistrationService(IRegistrationRepository registrationRepo, IStudentRepository studentRepo, ICourseSessionRepository courseSessionRepo, IUnitOfWork uow) : IRegistrationService
{
    public async Task<Result<int>> CountRegistrationsBySessionIdAsync(Guid courseSessionId, CancellationToken ct)
    {
        if (courseSessionId == Guid.Empty) return Result<int>.BadRequest("CourseSessionId is required.");

        var session = await courseSessionRepo.GetByIdAsync(courseSessionId, ct);
        if (session is null) return Result<int>.NotFound("Course session not found.");

        var count = await registrationRepo.CountBySessionIdAsync(courseSessionId, ct);
        return Result<int>.Ok(count);
    }

    public async Task<Result<IReadOnlyCollection<CourseSessionStudent>>> GetRegistrationsBySessionIdAsync(Guid courseSessionId, CancellationToken ct)
    {
        if (courseSessionId == Guid.Empty) return Result<IReadOnlyCollection<CourseSessionStudent>>.BadRequest("CourseSessionId is required.");

        var session = await courseSessionRepo.GetByIdAsync(courseSessionId, ct);
        if (session is null) return Result<IReadOnlyCollection<CourseSessionStudent>>.NotFound("Course session not found.");

        var regs = await registrationRepo.GetBySessionIdAsync(courseSessionId, ct);
        return Result<IReadOnlyCollection<CourseSessionStudent>>.Ok(regs);
    }

    public async Task<Result<IReadOnlyCollection<CourseSessionStudent>>> GetRegistrationsByStudentIdAsync(Guid studentId, CancellationToken ct)
    {
        if (studentId == Guid.Empty) return Result<IReadOnlyCollection<CourseSessionStudent>>.BadRequest("StudentId is required.");

        // valfritt men bra: bekräfta att student finns
        var student = await studentRepo.GetByIdAsync(studentId, ct);
        if (student is null) return Result<IReadOnlyCollection<CourseSessionStudent>>.NotFound("Student not found.");

        var regs = await registrationRepo.GetByStudentIdAsync(studentId, ct);
        return Result<IReadOnlyCollection<CourseSessionStudent>>.Ok(regs);
    }

    public async Task<Result<CourseSessionStudent>> RegisterStudentAsync(Guid courseSessionId, Guid studentId, CancellationToken ct)
    {
        if (courseSessionId == Guid.Empty) return Result<CourseSessionStudent>.BadRequest("CourseSessionId is required.");
        if (studentId == Guid.Empty) return Result<CourseSessionStudent>.BadRequest("StudentId is required.");

        var student = await studentRepo.GetByIdAsync(studentId, ct);
        if (student is null) return Result<CourseSessionStudent>.NotFound("Student not found.");

        var session = await courseSessionRepo.GetByIdAsync(courseSessionId, ct);
        if (session is null) return Result<CourseSessionStudent>.NotFound("Course session not found.");

        var alreadyRegistered = await registrationRepo.ExistsAsync(studentId, courseSessionId, ct);
        if (alreadyRegistered) return Result<CourseSessionStudent>.Conflict("Student is already registered on this course session.");

        var registration = new CourseSessionStudent(studentId, courseSessionId);

        var created = await registrationRepo.AddAsync(registration, ct);

        await uow.SaveChangesAsync(ct);

        return Result<CourseSessionStudent>.Ok(created);
    }

    public async Task<Result> UnregisterStudentAsync(Guid courseSessionId, Guid studentId, CancellationToken ct)
    {
        if (courseSessionId == Guid.Empty) return Result.BadRequest("CourseSessionId is required.");
        if (studentId == Guid.Empty) return Result.BadRequest("StudentId is required.");

        var exists = await registrationRepo.ExistsAsync(studentId, courseSessionId, ct);
        if (!exists) return Result.NotFound("Registration not found.");

        var removed = await registrationRepo.RemoveAsync(studentId, courseSessionId, ct);
        if(!removed) return Result.Conflict("Something went wrong.");

        await uow.SaveChangesAsync(ct);

        return Result.Ok();
    }
}
