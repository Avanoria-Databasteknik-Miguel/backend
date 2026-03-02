using CourseOnline.Application.Common.Interfaces;
using CourseOnline.Application.Common.Results;
using CourseOnline.Application.Contracts.StudentCourses;
using CourseOnline.Application.StudentCourses.Interfaces;
using CourseOnline.Domain.Models;

namespace CourseOnline.Application.Services;

public sealed class StudentCourseService(IStudentCourseRepository studentCoursesRepo, IUnitOfWork uow) : IStudentCourseService
{
    public async Task<Result> AddStudentToCourseAsync(Guid studentId, Guid courseId, CancellationToken ct)
    {
        if (studentId == Guid.Empty)
            return Result.BadRequest("StudentId is required.");

        if (courseId == Guid.Empty)
            return Result.BadRequest("CourseId is required.");

        if (await studentCoursesRepo.ExistsAsync(studentId, courseId, ct))
            return Result.Conflict("Student already assigned to course.");

        await studentCoursesRepo.AddAsync(studentId, courseId, ct);
        await uow.SaveChangesAsync(ct);

        return Result.Ok();
    }

    public async Task<Result<IReadOnlyCollection<Course>>> GetCoursesByStudentIdAsync(Guid studentId, CancellationToken ct)
    {
        if (studentId == Guid.Empty)
            return Result<IReadOnlyCollection<Course>>.BadRequest("StudentId is required.");

        var courses = await studentCoursesRepo.GetCoursesByStudentIdAsync(studentId, ct);
        return Result<IReadOnlyCollection<Course>>.Ok(courses);
    }

    public async Task<Result<IReadOnlyCollection<Student>>> GetStudentsByCourseIdAsync(Guid courseId, CancellationToken ct)
    {
        if (courseId == Guid.Empty)
            return Result<IReadOnlyCollection<Student>>.BadRequest("CourseId is required.");

        var students = await studentCoursesRepo.GetStudentsByCourseIdAsync(courseId, ct);
        return Result<IReadOnlyCollection<Student>>.Ok(students);
    }

    public async Task<Result> RemoveStudentFromCourseAsync(Guid studentId, Guid courseId, CancellationToken ct)
    {
        if (studentId == Guid.Empty)
            return Result.BadRequest("StudentId is required.");

        if (courseId == Guid.Empty)
            return Result.BadRequest("CourseId is required.");

        var removed = await studentCoursesRepo.RemoveAsync(studentId, courseId, ct);
        if (!removed)
            return Result.NotFound("StudentCourse relation not found.");

        await uow.SaveChangesAsync(ct);

        return Result.Ok();
    }
}
