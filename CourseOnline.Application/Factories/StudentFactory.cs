using CourseOnline.Application.Students.DTOs;
using CourseOnline.Domain.Models;
using System.Runtime.InteropServices;

namespace CourseOnline.Application.Factories;
public static class StudentFactory
{
    public static Student Create(CreateStudentInput input) => new(Guid.NewGuid(), input.FirstName, input.LastName, input.Email, input.ImageUrl, input.ProgramId);
}
