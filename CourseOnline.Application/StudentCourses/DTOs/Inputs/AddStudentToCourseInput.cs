namespace CourseOnline.Application.StudentCourses.DTOs.Inputs;

public sealed record AddStudentToCourseInput(Guid StudentId, Guid CourseId);