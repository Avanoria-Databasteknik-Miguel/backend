namespace CourseOnline.Application.StudentCourses.DTOs.Inputs;

public sealed record RemoveStudentFromCourseInput(Guid StudentId, Guid CourseId);
