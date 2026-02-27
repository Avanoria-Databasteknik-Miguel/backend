using CourseOnline.Application.Common.Results;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CourseOnline.Presentation.API.Common;

public static class ResultExtensions
{
    public static IResult ToHttpResult(this Result result)
    {
        if (result.Success) return Results.Ok();

        return result.ErrorType switch
        {
            ErrorTypes.BadRequest => Results.BadRequest(result.ErrorMessage),
            ErrorTypes.NotFound => Results.NotFound(result.ErrorMessage),
            ErrorTypes.Conflict => Results.Conflict(result.ErrorMessage),
            _ => Results.Problem(result.ErrorMessage)
        };
    }

    public static IResult ToHttpResult<T>(this Result<T> result)
    {
        if (result.Success) return Results.Ok(result.Value);

        return result.ErrorType switch 
        {
            ErrorTypes.BadRequest => Results.BadRequest(result.ErrorMessage),
            ErrorTypes.NotFound => Results.NotFound(result.ErrorMessage),
            ErrorTypes.Conflict => Results.Conflict(result.ErrorMessage),
            _ => Results.Problem(result.ErrorMessage)
        };
    }
}
