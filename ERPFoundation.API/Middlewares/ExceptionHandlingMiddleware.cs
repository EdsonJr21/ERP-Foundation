using System.Net;
using System.Text.Json;
using ERPFoundation.API.Responses;
using Microsoft.EntityFrameworkCore;

namespace ERPFoundation.API.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IWebHostEnvironment _environment;

    public ExceptionHandlingMiddleware(RequestDelegate next, IWebHostEnvironment environment)
    {
        _next = next;
        _environment = environment;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var statusCode = exception switch
        {
            ArgumentException => HttpStatusCode.BadRequest,
            KeyNotFoundException => HttpStatusCode.NotFound,
            DbUpdateException => HttpStatusCode.Conflict,
            _ => HttpStatusCode.InternalServerError
        };

        var message = exception switch
        {
            ArgumentException => "Invalid request data.",
            KeyNotFoundException => "Resource not found.",
            DbUpdateException => "Database update error.",
            _ => "An internal server error occurred."
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var response = new ErrorResponse
        {
            StatusCode = context.Response.StatusCode,
            Message = message,
            Detail = _environment.IsDevelopment()
                ? exception.Message
                : null
        };

        var json = JsonSerializer.Serialize(response);
        await context.Response.WriteAsync(json);
    }
}