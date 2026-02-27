namespace CourseOnline.Presentation.API.Models.Programs;
public sealed record CreateProgramRequest
{
    public required string Name { get; init; } = null!;
    public int? DurationWeeks { get; init; }
    public int? MaxStudent { get; init; }
}
