using CourseOnline.Application.Common.Results;
using CourseOnline.Application.Contracts.Courses;
using CourseOnline.Application.Courses.DTOs.Inputs;
using CourseOnline.Application.Courses.Interfaces;
using CourseOnline.Application.Factories;
using CourseOnline.Domain.Models;
using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace CourseOnline.Application.Services;

public sealed class CourseService(ICourseRepository courseRepo) : ICourseService
{
    public async Task<Result<Course>> CreateCourseAsync(CreateCourseInput input, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(input.Name)) return Result<Course>.BadRequest("Course name is required");
        if (input.DurationWeeks <= 0) return Result<Course>.BadRequest("Duration weeks must be higher than zero");
        if (input.MaxStudents <= 0) return Result<Course>.BadRequest("Limit of students must be higher than zero");


        var existingCourse = await courseRepo.GetByNameAsync(input.Name.ToLower(), ct);

        if (existingCourse is not null) return Result<Course>.Conflict("Course name already exist");

        var course = CourseFactory.Create(input);

        var createdCourse = await courseRepo.AddASync(course, ct);

        //TODO: cache stuff

        return Result<Course>.Ok(createdCourse);
    }

    public async Task<Result> DeleteCourseAsync(DeleteCourseInput input, CancellationToken ct)
    {
        if (input.Id == Guid.Empty) return Result.BadRequest("Id is required.");

        var deleteCourse = await courseRepo.GetByIdAsync(input.Id, ct);

        if (deleteCourse is null) return Result.NotFound("Course not found");

        var deleted = await courseRepo.RemoveAsync(deleteCourse.Id, ct);

        if (!deleted) return Result.Conflict("Something went wrong");

        return Result.Ok();
    }

    public async Task<Result<IReadOnlyCollection<Course>>> GetAllCoursesAsync(CancellationToken ct)
    {
        var courses = await courseRepo.GetAllAsync(ct);

        return Result<IReadOnlyCollection<Course>>.Ok(courses);
    }

    public async Task<Result<Course>> GetCourseByIdAsync(Guid id, CancellationToken ct)
    {
        if (id == Guid.Empty) return Result<Course>.BadRequest("Id is required");

        var course = await courseRepo.GetByIdAsync(id, ct);

        return course is null ? Result<Course>.NotFound("Course not founs") : Result<Course>.Ok(course);
    }

    public async Task<Result<Course>> GetCourseByNameAsync(string name, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(name)) return Result<Course>.BadRequest("Course name is required.");
        var course = await courseRepo.GetByNameAsync(name.ToLower(), ct);

        return course is null ? Result<Course>.NotFound("Course not found") : Result<Course>.Ok(course);

    }

    public async Task<Result<Course>> UpdateCourseAsync(UpdateCourseInput input, CancellationToken ct)
    {
        if (input.Id == Guid.Empty)
            return Result<Course>.BadRequest("Id is required.");

        if (string.IsNullOrWhiteSpace(input.Name))
            return Result<Course>.BadRequest("Course name is required.");

        if (input.DurationWeeks <= 0)
            return Result<Course>.BadRequest("Duration weeks must be higher than zero");

        if (input.MaxStudents <= 0)
            return Result<Course>.BadRequest("Limit of students must be higher than zero");

        var courseToUpdate = await courseRepo.GetByIdAsync(input.Id, ct);
        if (courseToUpdate is null)
            return Result<Course>.NotFound("Course not found");

       
        var normalizedName = input.Name.Trim().ToLower();
        var existingWithSameName = await courseRepo.GetByNameAsync(normalizedName, ct);
        if (existingWithSameName is not null && existingWithSameName.Id != input.Id)
            return Result<Course>.Conflict("Course name already exist");

    
        courseToUpdate.Update(
            name: normalizedName,
            durationWeeks: input.DurationWeeks,
            maxStudents: input.MaxStudents,
            teacherId: input.TeacherId,
            programId: input.ProgramId
        );

        var updated = await courseRepo.UpdateAsync(input.Id, courseToUpdate, ct);

        return updated is null
            ? Result<Course>.Conflict("Something went wrong")
            : Result<Course>.Ok(updated);
    }
}
