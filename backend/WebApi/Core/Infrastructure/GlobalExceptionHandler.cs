using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Core.Infrastructure;

/// <summary>
/// A global exception handler that captures unhandled exceptions and converts them
/// into standardized <see cref="ProblemDetails"/> responses.
/// </summary>
/// <param name="problemDetailsService">
/// The service responsible for writing <see cref="ProblemDetails"/> responses.
/// </param>
/// <param name="logger">The logger used to record exception details.</param>
public sealed class GlobalExceptionHandler(
    IProblemDetailsService problemDetailsService,
    ILogger<GlobalExceptionHandler> logger
) : IExceptionHandler
{
    /// <summary>
    /// Attempts to handle an unhandled exception by logging it and returning a standardized
    /// <see cref="ProblemDetails"/> response to the client.
    /// </summary>
    /// <param name="httpContext">The current HTTP context.</param>
    /// <param name="exception">The exception that occurred.</param>
    /// <param name="cancellationToken">A token used to cancel the operation.</param>
    /// <returns>
    /// <c>true</c> if the exception was successfully written as a problem details response;
    /// otherwise, <c>false</c>.
    /// </returns>
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken
    )
    {
        logger.LogError(exception, "Unhandled exception occurred");

        return await problemDetailsService.TryWriteAsync(
            new ProblemDetailsContext
            {
                HttpContext = httpContext,
                Exception = exception,
                ProblemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1",
                    Title = "Server failure",
                },
            }
        );
    }
}
