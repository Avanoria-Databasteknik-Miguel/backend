namespace CourseOnline.Application.Programs.DTOs.Inputs;
public sealed record UpdateProgramInput(Guid Id, string Name, int? DurationWeeks, int? MaxStudents)
