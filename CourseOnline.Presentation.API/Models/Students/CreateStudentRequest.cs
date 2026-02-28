namespace CourseOnline.Presentation.API.Models.Students;

public sealed record CreateStudentRequest(string FirstName, string LastName, string Email, string? ImageUrl, Guid? ProgramId);
