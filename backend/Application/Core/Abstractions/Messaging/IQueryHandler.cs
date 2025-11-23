using Domain.Core.Primitives;

namespace Application.Core.Abstractions.Messaging;

/// <summary>
/// Defines a handler for queries that return a response of the specified type.
/// </summary>
/// <typeparam name="TQuery">The type of query to handle.</typeparam>
/// <typeparam name="TResponse">The type of response returned by the query.</typeparam>
public interface IQueryHandler<in TQuery, TResponse>
    where TQuery : IQuery<TResponse>
{
    /// <summary>
    /// Handles the specified query.
    /// </summary>
    /// <param name="query">The query to handle.</param>
    /// <param name="cancellationToken">A token used to cancel the operation.</param>
    /// <returns>Task containing the result with the response.</returns>
    Task<Result<TResponse>> Handle(TQuery query, CancellationToken cancellationToken);
}
