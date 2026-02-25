using CourseOnline.Application.Teachers.DTOs.Inputs;
using CourseOnline.Domain.Models;

namespace CourseOnline.Application.Factories;
public static class TeacherFactory
{
    public static Teacher Create(CreateTeacherInput input) => new(Guid.NewGuid(), input.FirstName, input.LastName, input.Email, input.ImageUrl);
}
