using System.Net;
using System.Text.Json;
using Api.Exceptions;

namespace Api.Middleware;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlerMiddleware> _logger;
    private readonly IHostEnvironment _env;

    public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger, IHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception occurred!");

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = ex switch
            {
                ApiException apiEx => apiEx.StatusCode,
                _ => (int)HttpStatusCode.InternalServerError
            };

            var response = new
            {
                statusCode = context.Response.StatusCode,
                error = ex.GetType().Name,
                message = ex.Message,
                path = context.Request.Path,
                timestamp = DateTime.UtcNow,
                stackTrace = _env.IsDevelopment() ? ex.StackTrace : null
            };

            var json = JsonSerializer.Serialize(response, new JsonSerializerOptions { WriteIndented = true });
            await context.Response.WriteAsync(json);
        }
    }
}
