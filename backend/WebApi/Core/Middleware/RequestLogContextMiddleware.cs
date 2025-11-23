using Serilog.Context;

namespace WebApi.Core.Middleware;

/// <summary>
/// Middleware that enriches the logging context with a correlation ID
/// for each incoming HTTP request.
/// </summary>
/// <param name="next">The next middleware in the request pipeline.</param>
public class RequestLogContextMiddleware(RequestDelegate next)
{
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
            return next(context);
        }
    }
}
