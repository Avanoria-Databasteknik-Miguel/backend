using CourseOnline.Application.Classrooms.DTOs;
using CourseOnline.Application.Common.Results;
using CourseOnline.Domain.Models;

namespace CourseOnline.Application.Contracts.Classrooms;
public interface IClassroomService
{
    Task<Result<Classroom>> CreateClassroomAsync(CreateClassroomInput input, CancellationToken ct);
    Task<Result<Classroom>> UpdateClassroomAsync(UpdateClassroomInput input, CancellationToken ct);
    Task<Result> DeleteClassroomAsync(DeleteClassroomInput input, CancellationToken ct);
    Task<Result<Classroom>> GetClassroomByIdAsync(int Id, CancellationToken ct);
    Task<Result<IReadOnlyCollection<Classroom>>> GetAllClassroomsAsync(CancellationToken ct);
}
