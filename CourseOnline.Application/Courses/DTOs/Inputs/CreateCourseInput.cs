namespace CourseOnline.Application.Courses.DTOs.Inputs;

public sealed record CreateCourseInput(string Name, int? DurationWeeks, int? MaxStudents, Guid? TeacherId, Guid? ProgramId);
