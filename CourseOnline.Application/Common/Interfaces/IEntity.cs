namespace CourseOnline.Application.Common.Interfaces;

public interface IEntity<TId>
{
    TId Id { get; set; }
}

