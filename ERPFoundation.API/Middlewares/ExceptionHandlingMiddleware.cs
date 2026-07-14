using System.Net;
using ERPFoundation.API.Responses;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ERPFoundation.API.Middlewares;

public class ExceptionHandlingMiddleware(
    RequestDelegate next,
    IWebHostEnvironment environment,
    ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            if (context.Response.HasStarted)
            {
                logger.LogWarning(ex, "The response has already started; the exception cannot be handled.");
                throw;
            }

            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var statusCode = exception switch
        {
            ValidationException or ArgumentException => HttpStatusCode.BadRequest,
            KeyNotFoundException => HttpStatusCode.NotFound,
            DbUpdateException => HttpStatusCode.Conflict,
            _ => HttpStatusCode.InternalServerError
        };

        var message = exception switch
        {
            ValidationException => "Validation error.",
            ArgumentException or KeyNotFoundException => exception.Message,
            DbUpdateException => "Database update error.",
            _ => "An internal server error occurred."
        };

        if (statusCode == HttpStatusCode.InternalServerError)
        {
            logger.LogError(exception, "An unhandled exception occurred while processing {Method} {Path}.",
                context.Request.Method,
                context.Request.Path);
        }
        else
        {
            logger.LogWarning(exception, "Request {Method} {Path} failed with status code {StatusCode}.",
                context.Request.Method,
                context.Request.Path,
                (int)statusCode);
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        Dictionary<string, string[]>? errors = null;

        if (exception is ValidationException validationException)
        {
            errors = validationException.Errors
                .GroupBy(error => error.PropertyName)
                .ToDictionary(
                    group => group.Key,
                    group => group.Select(error => error.ErrorMessage).ToArray()
                );
        }

        var response = new ErrorResponse
        {
            StatusCode = context.Response.StatusCode,
            Message = message,
            Detail = environment.IsDevelopment()
                ? exception.Message
                : null,
            Errors = errors
        };

        await context.Response.WriteAsJsonAsync(response);
    }
}