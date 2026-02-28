namespace CourseOnline.Application.Courses.DTOs.Inputs;

public sealed record UpdateCourseInput(Guid Id,string Name, int? DurationWeeks, int? MaxStudents, Guid? TeacherId, Guid? ProgramId);
