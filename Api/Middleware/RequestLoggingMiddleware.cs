using System.Diagnostics;
using System.Net;
using System.Text;
using System.Text.Json;
using Base;

namespace Api.Middleware;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public RequestLoggingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
    {
        _next = next;
        _logger = loggerFactory.CreateLogger<RequestLoggingMiddleware>();

    }

    public async Task Invoke(HttpContext context)
    {
        var traceId = Guid.NewGuid().ToString();
        var sw = Stopwatch.StartNew();

        var requestText = await FormatRequest(context.Request);

        var originalBody = context.Response.Body;
        using var responseBody = new MemoryStream();
        context.Response.Body = responseBody;

        try
        {
            await _next(context);
            sw.Stop();

            var responseText = await FormatResponse(context.Response);

            var logText = new StringBuilder();
            logText.AppendLine("----- HTTP LOG START -----");
            logText.AppendLine($"TraceId     : {traceId}");
            logText.AppendLine($"Timestamp   : {DateTime.UtcNow}");
            logText.AppendLine($"Method      : {context.Request.Method}");
            logText.AppendLine($"Path        : {context.Request.Path}");
            logText.AppendLine($"StatusCode  : {context.Response.StatusCode}");
            logText.AppendLine($"Duration    : {sw.ElapsedMilliseconds} ms");
            logText.AppendLine($"Request     : {requestText}");
            logText.AppendLine($"Response    : {responseText}");
            logText.AppendLine("----- HTTP LOG END -----\n");

            // Console ve dosyaya logla
            _logger.LogInformation(logText.ToString());
            await File.AppendAllTextAsync("logs.txt", logText + Environment.NewLine);
        }
        catch (Exception ex)
        {
            sw.Stop();
            _logger.LogError($"TraceId: {traceId} | Exception: {ex.Message}");
            await HandleExceptionAsync(context, ex, traceId);
        }

        responseBody.Seek(0, SeekOrigin.Begin);
        await responseBody.CopyToAsync(originalBody);
    }

    private async Task<string> FormatRequest(HttpRequest request)
    {
        request.EnableBuffering();
        var body = await new StreamReader(request.Body).ReadToEndAsync();
        request.Body.Position = 0;
        return $"{request.Scheme} {request.Method} {request.Path} {request.QueryString} \nBody: {body}";
    }

    private async Task<string> FormatResponse(HttpResponse response)
    {
        response.Body.Seek(0, SeekOrigin.Begin);
        var body = await new StreamReader(response.Body).ReadToEndAsync();
        response.Body.Seek(0, SeekOrigin.Begin);
        return body;
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex, string traceId)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var error = new ErrorDetail
        {
            StatusCode = context.Response.StatusCode,
            Message = "Internal Server Error",
            TraceId = traceId
        };

        var json = JsonSerializer.Serialize(error);
        await context.Response.WriteAsync(json);
    }
}
