using CourseOnline.Domain.Exceptions;
using System.Security.Cryptography.X509Certificates;

namespace CourseOnline.Domain.Models;
public sealed class Teacher
{

    public string Id { get; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string? ImageUrl { get; set; }

    public Teacher(string? id, string firstName, string lastName,string email, string? imageUrl)
    {
        if (string.IsNullOrWhiteSpace(id)) throw new DomainValidationException("Id is required");
        if (string.IsNullOrWhiteSpace(firstName)) throw new DomainValidationException("First name is required");
        if (string.IsNullOrWhiteSpace(lastName)) throw new DomainValidationException("Last name is required");
        if (string.IsNullOrWhiteSpace(email)) throw new DomainValidationException("Email is required");

        Id = id.Trim();
        FirstName = firstName.Trim();
        LastName = lastName.Trim();
        Email = email.Trim();
        ImageUrl = string.IsNullOrWhiteSpace(imageUrl) ? "No image url" : imageUrl.Trim();
    }

    public void SetEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new DomainValidationException("Email is required.");

        Email = email;
    }

    public void Update(string firstName, string lastName, string? imageUrl)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new DomainValidationException("FirstName is required.");

        if (string.IsNullOrWhiteSpace(lastName))
            throw new DomainValidationException("LastName is required.");

        FirstName = firstName.Trim();
        LastName = lastName.Trim();
        ImageUrl = imageUrl?.Trim();
    }

}
