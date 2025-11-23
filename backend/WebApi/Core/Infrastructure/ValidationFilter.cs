using FluentValidation;
using FluentValidation.Results;

namespace WebApi.Core.Infrastructure;

/// <summary>
/// An endpoint filter that validates incoming requests of type <typeparamref name="TRequest"/>
/// using FluentValidation before invoking the endpoint handler.
/// </summary>
/// <typeparam name="TRequest">The type of request to validate.</typeparam>
/// <param name="validator">The validator used to validate the request.</param>
public sealed class ValidationFilter<TRequest>(IValidator<TRequest> validator)
    : IEndpointFilter
{
    /// <summary>
    /// Invokes the filter, validating the request before calling the next delegate in the pipeline.
    /// </summary>
    /// <param name="context">The <see cref="EndpointFilterInvocationContext"/> containing endpoint arguments.</param>
    /// <param name="next">The delegate representing the next filter or endpoint in the pipeline.</param>
    /// <returns>
    /// Returns a validation problem result if validation fails; otherwise, continues
    /// to the next filter or endpoint result.
    /// </returns>
    public async ValueTask<object?> InvokeAsync(
        EndpointFilterInvocationContext context,
        EndpointFilterDelegate next
    )
    {
        TRequest? request = context.Arguments.OfType<TRequest>().First();

        ValidationResult result = await validator.ValidateAsync(
            request,
            context.HttpContext.RequestAborted
        );

        if (!result.IsValid)
        {
            return TypedResults.ValidationProblem(result.ToDictionary());
        }

        return await next(context);
    }
}
