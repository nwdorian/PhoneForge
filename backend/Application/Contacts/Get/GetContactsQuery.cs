using Application.Core.Abstractions.Messaging;

namespace Application.Contacts.Get;

/// <summary>
/// The query for getting contacts.
/// </summary>
/// <param name="SearchTerm">The search term.</param>
/// <param name="Page">The current page.</param>
/// <param name="PageSize">The page size.</param>
/// <param name="SortOrder">The sort order.</param>
/// <param name="SortColumn">The sort column.</param>
public sealed record GetContactsQuery(
    string? SearchTerm,
    int Page,
    int PageSize,
    string SortColumn,
    string SortOrder
) : IQuery<GetContactsResponse>;
