namespace Application.Core.Abstractions.Messaging;

/// <summary>
/// Represents a query that returns a response of the specified type.
/// </summary>
/// <typeparam name="TResponse">The type of the response returned by the query.</typeparam>
public interface IQuery<TResponse>;
