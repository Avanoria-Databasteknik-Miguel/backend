namespace CourseOnline.Application.Programs.DTOs.Outputs;

public sealed record ProgramOutput(Guid Id, string Name, int? DurationWeeks, int? MaxStudents, DateTime CreatedAtUtc, DateTime ModifiedAtUtc);
