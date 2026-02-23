namespace CourseOnline.Domain.Exceptions;

public sealed class DomainValidationException(string message) : DomainException(message)
{
}
