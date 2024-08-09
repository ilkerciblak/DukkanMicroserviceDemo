
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuildingBlocks.Exceptions.Handler;


public class CustomExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
    {
        (string detail, string title, int code) details = exception switch
        {
            InternalServerException => (exception.Message, exception.GetType().Name,
                context.Response.StatusCode = StatusCodes.Status500InternalServerError),
            ValidationException => (exception.Message, exception.GetType().Name,
                context.Response.StatusCode = StatusCodes.Status400BadRequest),
            BadRequestException => (exception.Message, exception.GetType().Name,
                context.Response.StatusCode = StatusCodes.Status400BadRequest),
            NotFoundException => (exception.Message, exception.GetType().Name,
                context.Response.StatusCode = StatusCodes.Status404NotFound),
            _ => (exception.Message, exception.GetType().Name,
                context.Response.StatusCode = StatusCodes.Status400BadRequest)
        };

        var problemDetails = new ProblemDetails
        {
            Title = details.title,
            Status = context.Response.StatusCode,
            Detail = details.detail,
            Instance = context.Request.Path,

        };
        
        problemDetails.Extensions.Add("traceId", context.TraceIdentifier);

        if (exception is ValidationException validationException)
        {
            problemDetails.Extensions.Add("ValidationErrors", validationException.Errors);
        }

        await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}