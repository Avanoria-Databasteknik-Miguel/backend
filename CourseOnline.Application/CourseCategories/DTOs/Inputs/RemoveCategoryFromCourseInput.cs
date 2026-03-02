namespace CourseOnline.Application.CourseCategories.DTOs.Inputs;

public sealed record RemoveCategoryFromCourseInput(Guid CourseId, Guid CategoryId);
