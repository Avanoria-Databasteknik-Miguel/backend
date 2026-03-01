namespace CourseOnline.Application.Classrooms.DTOs;

public sealed record CreateClassroomInput(string Name, int Seats, int FloorId);