using WebApi.Core.Constants;

namespace WebApi.Contacts.Get;

/// <summary>
/// Represents the request for getting a list of contacts.
/// </summary>
/// <param name="SearchTerm">The search term.</param>
/// <param name="Page">The current page.</param>
/// <param name="PageSize">The page size.</param>
/// <param name="SortColumn">The sorting column.</param>
/// <param name="SortOrder">The sorting order.</param>
public sealed record GetContactsRequest(
    string? SearchTerm,
    int Page = Pagination.DefaultPage,
    int PageSize = Pagination.DefaultPageSize,
    string SortColumn = Pagination.DefaultSortColum,
    string SortOrder = Pagination.DefaultSortOrder
);
