namespace CourseOnline.Application.Students.DTOs;

public sealed record CreateStudentInput(string FirstName, string LastName, string Email, string? ImageUrl, Guid? ProgramId);
