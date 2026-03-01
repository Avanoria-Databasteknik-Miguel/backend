namespace CourseOnline.Application.Classrooms.DTOs;

public sealed record UpdateClassroomInput(int Id, string Name, int Seats, int FloorId);
