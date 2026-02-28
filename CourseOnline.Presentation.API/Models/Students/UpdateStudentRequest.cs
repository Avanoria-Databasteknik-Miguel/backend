namespace CourseOnline.Presentation.API.Models.Students;

public sealed record UpdateStudentRequest(string FirstName, string LastName, string Email, string? ImageUrl, Guid? ProgramId);
