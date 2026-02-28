using CourseOnline.Application.Courses.DTOs.Inputs;
using CourseOnline.Domain.Models;

namespace CourseOnline.Application.Factories;

public static class CourseFactory
{
    public static Course Create(CreateCourseInput input) => new(Guid.NewGuid(), input.Name, input.DurationWeeks, input.MaxStudents, input.TeacherId, input.ProgramId);
}
