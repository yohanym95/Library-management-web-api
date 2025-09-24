using System.Net;
using System.Text.Json;

namespace Library.Management.API.Middlewares;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context); // Pass request to next middleware
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An unhandled exception occurred: {ex.Message}");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        HttpStatusCode status = HttpStatusCode.InternalServerError;
        string message = "Internal Server Error";


        if (exception is KeyNotFoundException)
        {
            status = HttpStatusCode.NotFound;
            message = exception.Message;
        }

        context.Response.StatusCode = (int) status;

        var result = JsonSerializer.Serialize(new
        {
            error = message,
            status = context.Response.StatusCode
        });

        return context.Response.WriteAsync(result);
    }

}