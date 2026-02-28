namespace CourseOnline.Presentation.API.Models.Courses;

public sealed record CreateCourseRequest(string Name, int? DurationWeeks, int? MaxStudents, Guid? TeacherId, Guid? ProgramId);
