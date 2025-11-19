using FluentValidation;

namespace WebApi.Core.Infrastructure;

/// <summary>
/// An endpoint filter that validates incoming requests of type <typeparamref name="TRequest"/>
/// using FluentValidation before invoking the endpoint handler.
/// </summary>
/// <typeparam name="TRequest">The type of request to validate.</typeparam>
public sealed class ValidationFilter<TRequest> : IEndpointFilter
{
    private readonly IValidator<TRequest> _validator;

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationFilter{TRequest}"/> class.
    /// </summary>
    /// <param name="validator">The validator used to validate the request.</param>
    public ValidationFilter(IValidator<TRequest> validator)
    {
        _validator = validator;
    }

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
        var request = context.Arguments.OfType<TRequest>().First();

        var result = await _validator.ValidateAsync(
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
