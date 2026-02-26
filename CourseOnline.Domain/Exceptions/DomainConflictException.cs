namespace CourseOnline.Domain.Exceptions;
public sealed class DomainConflictException(string message) : DomainException(message) 
{
}
