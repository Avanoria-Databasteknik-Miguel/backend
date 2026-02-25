namespace CourseOnline.Application.Teachers.DTOs.Inputs;

public sealed record CreateTeacherInput
(
    string FirstName,
    string LastName,
    string Email,
    string? ImageUrl
);
