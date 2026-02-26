namespace CourseOnline.Domain.Exceptions;
public sealed class DomainNotFoundException(string message) : DomainException(message)
{
}
