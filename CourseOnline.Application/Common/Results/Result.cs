namespace CourseOnline.Application.Common.Results;
public sealed record Result(bool Success, ErrorTypes? ErrorType = null, string? ErrorMessage = null)
{
    public static Result Ok() => new(true);
    public static Result BadRequest(string message) => new(false, ErrorTypes.BadRequest, message);
    public static Result NotFound(string message) => new(false, ErrorTypes.NotFound, message);
    public static Result Conflict(string message) => new(false, ErrorTypes.Conflict, message);
    public static Result Unexpected(string message) => new(false, ErrorTypes.Unexpected, message);

}

public sealed record Result<T>(bool Success, T? Value = default, ErrorTypes? ErrorType = null, string? ErrorMessage = null)
{
    public static Result<T> Ok() => new(true);
    public static Result<T> BadRequest(string message) => new(false, default, ErrorTypes.BadRequest, message);
    public static Result<T> NotFound(string message) => new(false, default, ErrorTypes.NotFound, message);
    public static Result<T> Conflict(string message) => new(false, default, ErrorTypes.Conflict, message);
    public static Result<T> Unexpected(string message) => new(false, default, ErrorTypes.Unexpected, message);
} 