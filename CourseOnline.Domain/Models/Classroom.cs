using CourseOnline.Domain.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace CourseOnline.Domain.Models;
public sealed class Classroom
{
    public int Id { get; }
    public string Name { get; private set; }
    public int Seats { get; private set; }
    public int FloorId { get; private set; }

    public Classroom(int id, string name, int seats, int floorId)
    {
        if (id <= 0) throw new DomainValidationException("Id is required");
        if (string.IsNullOrWhiteSpace(name)) throw new DomainValidationException("Classroom name is required");
        if (seats <= 0) throw new DomainValidationException("Seats must be greater than 0");
        if (floorId <= 0) throw new DomainValidationException("FloorId is required");

        Id = id;
        Name = name.Trim().ToLower();
        Seats = seats;
        FloorId = floorId;
    }

    public void Update(string name, int seats, int floorId)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new DomainValidationException("Classroom name is required");
        if (seats <= 0) throw new DomainValidationException("Seats must be greater than 0");
        if (floorId <= 0) throw new DomainValidationException("FloorId is required");

        Name = name.Trim().ToLower();
        Seats = seats;
        FloorId = floorId;
    } 
}
