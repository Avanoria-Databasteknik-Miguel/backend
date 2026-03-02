using CourseOnline.Application.Classrooms.Interfaces;
using CourseOnline.Application.Common.Interfaces;
using CourseOnline.Application.Common.Results;
using CourseOnline.Application.Contracts.CourseSessions;
using CourseOnline.Application.Courses.Interfaces;
using CourseOnline.Application.CourseSessions.DTOs.Inputs;
using CourseOnline.Application.CourseSessions.DTOs.Outputs;
using CourseOnline.Application.CourseSessions.Interfaces;
using CourseOnline.Application.Factories;
using CourseOnline.Domain.Models;

namespace CourseOnline.Application.Services;

public sealed class CourseSessionService(ICourseSessionRepository courseSessionRepo, ICourseRepository courseRepo, IClassroomsRepository classroomRepo, IUnitOfWork uow) : ICourseSessionService
{
    public async Task<Result<CourseSession>> CreateCourseSessionAsync(CreateCourseSessionInput input, CancellationToken ct)
    {
        if (input.CourseId == Guid.Empty) return Result<CourseSession>.BadRequest("CourseId is required.");
        if (input.ClassroomId <= 0) return Result<CourseSession>.BadRequest("ClassroomId is required.");
        if (input.StartDateTimeUtc >= input.EndDateTimeUtc) return Result<CourseSession>.BadRequest("Start must be before End.");

        var course = await courseRepo.GetByIdAsync(input.CourseId, ct);
        if (course is null) return Result<CourseSession>.NotFound("Course not found.");

        var classroom = await classroomRepo.GetByIdAsync(input.ClassroomId, ct);
        if (classroom is null) return Result<CourseSession>.NotFound("Classroom not found.");

        var session = CourseSessionFactory.Create(input);

        var created = await courseSessionRepo.AddASync(session, ct);

        await uow.SaveChangesAsync(ct);

        return Result<CourseSession>.Ok(created);
    }

    public async Task<Result<CourseSession>> UpdateCourseSessionAsync(UpdateCourseSessionInput input, CancellationToken ct)
    {
        if (input.Id == Guid.Empty) return Result<CourseSession>.BadRequest("Id is required.");
        if (input.CourseId == Guid.Empty) return Result<CourseSession>.BadRequest("CourseId is required.");
        if (input.ClassroomId <= 0) return Result<CourseSession>.BadRequest("ClassroomId is required.");
        if (input.StartDateTimeUtc >= input.EndDateTimeUtc) return Result<CourseSession>.BadRequest("Start must be before End.");

        var existing = await courseSessionRepo.GetByIdAsync(input.Id, ct);
        if (existing is null) return Result<CourseSession>.NotFound("Course session not found.");

        // Om CourseId får ändras: verify it exists
        var course = await courseRepo.GetByIdAsync(input.CourseId, ct);
        if (course is null) return Result<CourseSession>.NotFound("Course not found.");

        var classroom = await classroomRepo.GetByIdAsync(input.ClassroomId, ct);
        if (classroom is null) return Result<CourseSession>.NotFound("Classroom not found.");

        existing.Update(
            input.StartDateTimeUtc,
            input.EndDateTimeUtc
        );

        var updated = await courseSessionRepo.UpdateAsync(input.Id, existing, ct);
        if(updated is null) return Result<CourseSession>.Conflict("Something went wrong.");

        await uow.SaveChangesAsync(ct);

        return Result<CourseSession>.Ok(updated);
    }

    public async Task<Result> DeleteCourseSessionAsync(DeleteCourseSessionInput input, CancellationToken ct)
    {
        if (input.Id == Guid.Empty) return Result.BadRequest("Id is required.");

        var existing = await courseSessionRepo.GetByIdAsync(input.Id, ct);
        if (existing is null) return Result.NotFound("Course session not found.");

        var deleted = await courseSessionRepo.RemoveAsync(existing.Id, ct);
        if (!deleted) return Result.Conflict("Something went wrong.");

        await uow.SaveChangesAsync(ct);

        return Result.Ok();
    }

    public async Task<Result<CourseSession>> GetCourseSessionByIdAsync(Guid id, CancellationToken ct)
    {
        if (id == Guid.Empty) return Result<CourseSession>.BadRequest("Id is required.");

        var session = await courseSessionRepo.GetByIdAsync(id, ct);
        return session is null
            ? Result<CourseSession>.NotFound("Course session not found.")
            : Result<CourseSession>.Ok(session);
    }

    public async Task<Result<IReadOnlyCollection<CourseSession>>> GetAllCourseSessionsAsync(CancellationToken ct)
    {
        var courseSessions = await courseSessionRepo.GetAllAsync(ct);

        return Result<IReadOnlyCollection<CourseSession>>.Ok(courseSessions);
    }

    public async Task<Result<IReadOnlyCollection<CourseSessionWithCourseOutput>>> GetCourseSessionsByCourseIdAsync(Guid courseId, CancellationToken ct)
    {
        if (courseId == Guid.Empty)
            return Result<IReadOnlyCollection<CourseSessionWithCourseOutput>>.BadRequest("CourseId is required.");

        // 1) Hämta kursen (för att kunna stoppa in hela objektet)
        var course = await courseRepo.GetByIdAsync(courseId, ct);
        if (course is null)
            return Result<IReadOnlyCollection<CourseSessionWithCourseOutput>>.NotFound("Course not found.");

        // 2) Hämta alla sessions för kursen
        var sessionsResult = await courseSessionRepo.GetByCourseIdAsync(courseId, ct);
        if (!sessionsResult.Success)
            return Result<IReadOnlyCollection<CourseSessionWithCourseOutput>>.NotFound("Course sessions not found.");

        var sessions = sessionsResult.Value ?? [];

        // 3) Mappa till “expanded output”
        IReadOnlyCollection<CourseSessionWithCourseOutput> output = [.. sessions
            .Select(s => new CourseSessionWithCourseOutput(
                s.Id,
                s.CourseId,
                s.ClassroomId,
                s.StartDateTimeUtc,
                s.EndDateTimeUtc,
                course
            ))];

        return Result<IReadOnlyCollection<CourseSessionWithCourseOutput>>.Ok(output);
    }
}
