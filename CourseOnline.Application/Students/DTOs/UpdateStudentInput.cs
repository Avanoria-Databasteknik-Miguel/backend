namespace CourseOnline.Application.Students.DTOs;

public sealed record UpdateStudentInput(Guid Id, string FirstName, string LastName, string Email, string? ImageUrl, Guid? ProgramId);
