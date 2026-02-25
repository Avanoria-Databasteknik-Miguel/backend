namespace CourseOnline.Presentation.API.Models.Teachers;
public sealed record CreateTeacherRequest
{
    public required string FirstName { get; init; } = null!;
    public required string LastName { get; init; } = null!;
    public required string Email { get; init; } = null!;
    public string? ImageUrl { get; init; } = null!;
}
