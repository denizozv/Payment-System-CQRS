using System.Net;

namespace Api.Middleware;

public class HeartBeatMiddleware
{
    public readonly RequestDelegate next;

    public HeartBeatMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Path.StartsWithSegments("/api/Customers/GetById/99"))
        {
            context.Response.ContentType = "text/plain";
            context.Response.StatusCode = (int)HttpStatusCode.OK;
            await context.Response.WriteAsync("Hello World!");
            return;
        }
        
        await next(context);
    }
}