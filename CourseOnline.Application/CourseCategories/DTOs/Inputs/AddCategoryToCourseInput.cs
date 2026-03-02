namespace CourseOnline.Application.CourseCategories.DTOs.Inputs;

public sealed record AddCategoryToCourseInput(Guid CourseId,Guid CategoryId);
