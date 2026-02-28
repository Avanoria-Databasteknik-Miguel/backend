using CourseOnline.Domain.Exceptions;

namespace CourseOnline.Domain.Models;
public sealed class Student
{
    public Guid Id { get; }
    public Guid? ProgramId { get; private set; }  

    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string? ImageUrl { get; private set; }

    public Student(Guid id, string firstName, string lastName, string email, string? imageUrl, Guid? programId)
    {
        if (id == Guid.Empty) throw new DomainValidationException("Id is required");
        if (string.IsNullOrWhiteSpace(firstName)) throw new DomainValidationException("First name is required");
        if (string.IsNullOrWhiteSpace(lastName)) throw new DomainValidationException("Last name is required");
        if (string.IsNullOrWhiteSpace(email)) throw new DomainValidationException("Email is required");

        Id = id;
        FirstName = firstName.Trim();
        LastName = lastName.Trim();
        Email = email.Trim();
        ImageUrl = string.IsNullOrWhiteSpace(imageUrl) ? null : imageUrl.Trim();
        ProgramId = programId;

    }

    public void SetEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new DomainValidationException("Email is required.");

        Email = email.Trim();
    }


    public void Update(string firstName, string lastName, string? imageUrl, Guid? programId)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new DomainValidationException("FirstName is required.");

        if (string.IsNullOrWhiteSpace(lastName))
            throw new DomainValidationException("LastName is required.");

        FirstName = firstName.Trim();
        LastName = lastName.Trim();
        ImageUrl = imageUrl?.Trim();
        ProgramId = programId;
    }

    //public void UpdateProgram();

}
