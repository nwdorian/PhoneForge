using Serilog.Context;

namespace WebApi.Core.Middleware;

/// <summary>
/// Middleware that enriches the logging context with a correlation ID
/// for each incoming HTTP request.
/// </summary>
public class RequestLogContextMiddleware
{
    private readonly RequestDelegate _next;

    /// <summary>
    /// Initializes a new instance of the <see cref="RequestLogContextMiddleware"/> class.
    /// </summary>
    /// <param name="next">The next middleware in the request pipeline.</param>
    public RequestLogContextMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    /// <summary>
    /// Adds the current request's correlation ID to the Serilog log context
    /// and invokes the next middleware in the pipeline.
    /// </summary>
    /// <param name="context">The HTTP context for the current request.</param>
    /// <returns>A task representing the asynchronous middleware operation.</returns>
    public Task InvokeAsync(HttpContext context)
    {
        using (LogContext.PushProperty("CorrelationId", context.TraceIdentifier))
        {
            return _next(context);
        }
    }
}
