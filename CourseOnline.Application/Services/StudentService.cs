using CourseOnline.Application.Common.Results;
using CourseOnline.Application.Contracts.Students;
using CourseOnline.Application.Factories;
using CourseOnline.Application.Students.DTOs;
using CourseOnline.Application.Students.Interfaces;
using CourseOnline.Domain.Models;

namespace CourseOnline.Application.Services;

public sealed class StudentService(IStudentRepository studentRepo) : IStudentService
{
    public async Task<Result<Student>> CreateStudentAsync(CreateStudentInput input, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(input.FirstName)) return Result<Student>.BadRequest("First name is required.");
        if (string.IsNullOrWhiteSpace(input.LastName)) return Result<Student>.BadRequest("Last name is required.");
        if (string.IsNullOrWhiteSpace(input.Email)) return Result<Student>.BadRequest("Email is required.");

        var studentExist = await studentRepo.GetByEmailAsync(input.Email, ct);

        if (studentExist is not null) return Result<Student>.Conflict("Student already exist");

        var student = StudentFactory.Create(input);

        var createdStudent = await studentRepo.AddASync(student, ct);

        return Result<Student>.Ok(createdStudent);
    }

    public async Task<Result> DeleteStudentAsync(Guid id, CancellationToken ct)
    {
        if (id == Guid.Empty) return Result.BadRequest("Email is required.");
        var studentToDelete = await studentRepo.GetByIdAsync(id, ct);
        if (studentToDelete is null) return Result.BadRequest("Student id doesn't exist, can't delete teacher.");

        var deleted = await studentRepo.RemoveAsync(studentToDelete.Id, ct);

        if (!deleted) return Result.Conflict("Something went wrong");
        return Result.Ok();

    }

    public async Task<Result<Student>> GetStudentByIdAsync(Guid id, CancellationToken ct)
    {
        if (id == Guid.Empty) return null!;

        var student = await studentRepo.GetByIdAsync(id, ct);

        return student is null ? Result<Student>.NotFound("Student not found") : Result<Student>.Ok(student);
    }

    public async Task<Result<IReadOnlyCollection<Student>>> GetStudentsAsync(CancellationToken ct)
    {
        var students = await studentRepo.GetAllAsync(ct);

        return Result<IReadOnlyCollection<Student>>.Ok(students);
    }

    public async Task<Result<Student>> UpdateStudentAsync(UpdateStudentInput input, CancellationToken ct)
    {
        if (input.Id == Guid.Empty) return Result<Student>.BadRequest("Id required");

        var studentToUpdate = await studentRepo.GetByIdAsync(input.Id, ct);
        if (studentToUpdate is null) return Result<Student>.NotFound("Student not found");


        studentToUpdate.Update(input.FirstName, input.LastName, input.ImageUrl, input.ProgramId);
        studentToUpdate.SetEmail(input.Email); // only if email is allowed to change

        var updatedStudent = await studentRepo.UpdateAsync(input.Id, studentToUpdate, ct);



        return updatedStudent is null ? Result<Student>.Conflict("Something wrong happened") : Result<Student>.Ok(updatedStudent);
    }
}
