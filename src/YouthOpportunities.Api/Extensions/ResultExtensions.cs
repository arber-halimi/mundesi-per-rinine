using Microsoft.AspNetCore.Mvc;
using YouthOpportunities.Application.Common.Results;

namespace YouthOpportunities.Api.Extensions;

public static class ResultExtensions
{
    public static IActionResult ToActionResult<T>(
        this Result<T> result,
        ControllerBase controller,
        Func<T, IActionResult> onSuccess)
    {
        if (result.Succeeded)
        {
            return onSuccess(result.Data!);
        }

        return ToProblemDetails(result, controller);
    }

    public static IActionResult ToActionResult(
        this Result result,
        ControllerBase controller,
        Func<IActionResult> onSuccess)
    {
        if (result.Succeeded)
        {
            return onSuccess();
        }

        return ToProblemDetails(result, controller);
    }

    private static IActionResult ToProblemDetails(Result result, ControllerBase controller)
    {
        var statusCode = ToStatusCode(result.Status);

        return controller.Problem(
            statusCode: statusCode,
            title: ToTitle(result.Status),
            detail: result.Message,
            extensions: new Dictionary<string, object?>
            {
                ["code"] = result.ErrorCode
            });
    }

    private static int ToStatusCode(ResultStatus status)
    {
        return status switch
        {
            ResultStatus.Validation => StatusCodes.Status400BadRequest,
            ResultStatus.NotFound => StatusCodes.Status404NotFound,
            ResultStatus.Conflict => StatusCodes.Status409Conflict,
            ResultStatus.Unauthorized => StatusCodes.Status401Unauthorized,
            ResultStatus.Forbidden => StatusCodes.Status403Forbidden,
            _ => StatusCodes.Status400BadRequest
        };
    }

    private static string ToTitle(ResultStatus status)
    {
        return status switch
        {
            ResultStatus.Validation => "Validation error",
            ResultStatus.NotFound => "Not found",
            ResultStatus.Conflict => "Conflict",
            ResultStatus.Unauthorized => "Unauthorized",
            ResultStatus.Forbidden => "Forbidden",
            _ => "Request failed"
        };
    }
}