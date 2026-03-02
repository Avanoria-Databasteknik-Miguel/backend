using CourseOnline.Application.Categories.DTOs.Outputs;

namespace CourseOnline.Application.CourseCategories.DTOs.Outputs;

public sealed record CourseWithCategoriesOutput(Guid Id, string Name, int? DurationWeeks, int? MaxStudents, Guid? TeacherId, Guid? ProgramId, IReadOnlyCollection<CategoryOutput> Categories);
