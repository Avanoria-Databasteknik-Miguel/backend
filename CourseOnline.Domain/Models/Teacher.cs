using CourseOnline.Domain.Exceptions;

namespace CourseOnline.Domain.Models;
public sealed class Teacher
{

    public Guid Id { get; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string? ImageUrl { get; set; }

    public Teacher(Guid id, string firstName, string lastName,string email, string? imageUrl)
    {
        if (id == Guid.Empty) throw new DomainValidationException("Id is required");
        if (string.IsNullOrWhiteSpace(firstName)) throw new DomainValidationException("First name is required");
        if (string.IsNullOrWhiteSpace(lastName)) throw new DomainValidationException("Last name is required");
        if (string.IsNullOrWhiteSpace(email)) throw new DomainValidationException("Email is required");

        Id = id;
        FirstName = firstName.Trim().ToLower();
        LastName = lastName.Trim().ToLower();
        Email = email.Trim().ToLower();
        ImageUrl = string.IsNullOrWhiteSpace(imageUrl) ? null : imageUrl.Trim();
    }

    public void SetEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new DomainValidationException("Email is required.");

        Email = email.Trim();
    }

    public void Update(string firstName, string lastName, string? imageUrl)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new DomainValidationException("FirstName is required.");

        if (string.IsNullOrWhiteSpace(lastName))
            throw new DomainValidationException("LastName is required.");

        FirstName = firstName.Trim().ToLower();
        LastName = lastName.Trim().ToLower();
        ImageUrl = imageUrl?.Trim().ToLower();
    }

}
