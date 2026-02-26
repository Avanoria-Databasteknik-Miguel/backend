using CourseOnline.Application.Programs.DTOs.Inputs;
using CourseOnline.Domain.Models;

namespace CourseOnline.Application.Factories;
public static class ProgramFactory
{
    public static Program Create(CreateProgramInput input) => new(Guid.NewGuid(), input.Name, input.DurationWeeks, input.MaxStudents);

}
