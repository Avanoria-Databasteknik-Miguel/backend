namespace CourseOnline.Application.Teachers.DTOs.Inputs;
public sealed record UpdateTeacherInput
(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string ImageUrl
);
