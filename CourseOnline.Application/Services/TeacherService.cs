using CourseOnline.Application.Contracts.Teachers;
using CourseOnline.Application.Factories;
using CourseOnline.Application.Teachers.DTOs.Inputs;
using CourseOnline.Application.Teachers.Interfaces;
using CourseOnline.Domain.Models;

namespace CourseOnline.Application.Services;

public sealed class TeacherService(ITeacherRepository teacherRepo) : ITeacherService
{
    public async Task<Teacher?> CreateTeacherAsync(CreateTeacherInput input, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(input.FirstName)) return null!;
        if (string.IsNullOrWhiteSpace(input.LastName)) return null!;
        if (string.IsNullOrWhiteSpace(input.Email)) return null!;

        var teacherExits = await teacherRepo.GetByEmailAsync(input.Email, ct);

        if (teacherExits is null) return null!;

        var teacher = TeacherFactory.Create(input);

        var teacherCreated = await teacherRepo.AddASync(teacher, ct);

        if (teacherCreated is null) return null!;

        //TODO: cache stuff.

        return teacherCreated;
    }

    public async Task<bool> DeleteTeacherAsync(Guid id, CancellationToken ct)
    {
        if (id == Guid.Empty) return false;

        var teacherToDelete = await teacherRepo.GetByIdAsync(id, ct);

        if (teacherToDelete is null) return false;
        
        var deleted = await teacherRepo.RemoveAsync(teacherToDelete.Id, ct);

        if (!deleted) return false;

        //Todo: Cache stuff;

        return true;

    }

    public async Task<IReadOnlyCollection<Teacher>> GetTeachersAsync(CancellationToken ct)
    {
        var teachers = await teacherRepo.GetAllAsync(ct);

        return teachers.Any() ? teachers : [];
    }

    public async Task<Teacher?> GetTeacherByEmail(string email, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(email)) return null!;

        var teacher = await teacherRepo.GetByEmailAsync(email, ct);

        return teacher is null ? null : teacher;
    }

    public async Task<Teacher?> GetTeacherByIdAsync(Guid id, CancellationToken ct)
    {
        if (id == Guid.Empty) return null!;

        var teacher = await teacherRepo.GetByIdAsync(id, ct);

        return teacher is null ? null : teacher;
    }

    public async Task<Teacher?> UpdateTeacher(UpdateTeacherInput input, CancellationToken ct)
    {
        if (input.Id == Guid.Empty) return null!;

        var teacher = await teacherRepo.GetByIdAsync(input.Id, ct);

        return teacher is null ? null : teacher;
    }
}
