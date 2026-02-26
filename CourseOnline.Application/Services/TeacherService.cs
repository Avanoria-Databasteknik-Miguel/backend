using CourseOnline.Application.Common.Results;
using CourseOnline.Application.Contracts.Teachers;
using CourseOnline.Application.Factories;
using CourseOnline.Application.Teachers.DTOs.Inputs;
using CourseOnline.Application.Teachers.Interfaces;
using CourseOnline.Domain.Models;

namespace CourseOnline.Application.Services;

public sealed class TeacherService(ITeacherRepository teacherRepo) : ITeacherService
{
    public async Task<Result<Teacher>> CreateTeacherAsync(CreateTeacherInput input, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(input.FirstName)) return Result<Teacher>.BadRequest("First name is required.");
        if (string.IsNullOrWhiteSpace(input.LastName)) return Result<Teacher>.BadRequest("Last name is required.");
        if (string.IsNullOrWhiteSpace(input.Email)) return Result<Teacher>.BadRequest("Email is required.");

        var teacherExits = await teacherRepo.GetByEmailAsync(input.Email, ct);

        if (teacherExits is not null) return Result<Teacher>.Conflict("Teacher already exist");

        var teacher = TeacherFactory.Create(input);

        var teacherCreated = await teacherRepo.AddASync(teacher, ct);


        //if (teacherCreated is null) return Result<Teacher>.Conflict("Something wrong happened");

        //TODO: cache stuff.

        return Result<Teacher>.Ok(teacherCreated);
    }

    public async Task<Result> DeleteTeacherAsync(Guid id, CancellationToken ct)
    {
        if (id == Guid.Empty) return Result.BadRequest("Email is required.");

        var teacherToDelete = await teacherRepo.GetByIdAsync(id, ct);

        if (teacherToDelete is null) return Result.BadRequest("Teacher id doesn't exist, can't delete teacher.");
        
        var deleted = await teacherRepo.RemoveAsync(teacherToDelete.Id, ct);

        if (!deleted) return Result.BadRequest("Something went wrong");

        //Todo: Cache stuff;

        return Result.Ok();

    }

    public async Task<IReadOnlyCollection<Teacher>> GetTeachersAsync(CancellationToken ct)
    {
        var teachers = await teacherRepo.GetAllAsync(ct);

        return teachers.Any() ? teachers : [];
    }

    public async Task<Result<Teacher>> GetTeacherByEmail(string email, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(email)) return Result<Teacher>.BadRequest("Email is required.");

        var teacher = await teacherRepo.GetByEmailAsync(email, ct);

        return teacher is null ? Result<Teacher>.Conflict("Something wrong happened") : Result<Teacher>.Ok(teacher);
    }

    public async Task<Result<Teacher>> GetTeacherByIdAsync(Guid id, CancellationToken ct)
    {
        if (id == Guid.Empty) return null!;

        var teacher = await teacherRepo.GetByIdAsync(id, ct);

        return teacher is null ? Result<Teacher>.Conflict("Teacher not found") : Result<Teacher>.Ok(teacher);
    }

    public async Task<Result<Teacher>> UpdateTeacherAsync(UpdateTeacherInput input, CancellationToken ct)
    {
        if (input.Id == Guid.Empty) return Result<Teacher>.BadRequest("Id required");

        var teacherToUpdate = await teacherRepo.GetByIdAsync(input.Id, ct);
        if (teacherToUpdate is null) return Result<Teacher>.BadRequest("Teacher not found");


        teacherToUpdate.Update(input.FirstName, input.LastName, input.ImageUrl);
        teacherToUpdate.SetEmail(input.Email); // only if email is allowed to change

        var updatedTeacher = await teacherRepo.UpdateAsync(input.Id, teacherToUpdate, ct);



        return updatedTeacher is null ? Result<Teacher>.Conflict("Something wrong happened") : Result<Teacher>.Ok(updatedTeacher);


    }
}
