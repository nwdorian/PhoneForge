namespace Application.Contacts.Get;

/// <summary>
/// A response for returning paged list of contacts.
/// </summary>
/// <param name="Page">Current page.</param>
/// <param name="PageSize">Page size.</param>
/// <param name="TotalCount">Total number of items in all pages.</param>
/// <param name="TotalPages">Total number of pages.</param>
/// <param name="HasNextPage">Indicates whether next page exists.</param>
/// <param name="HasPreviousPage">Indicates whether previous page exists.</param>
/// <param name="Items">List of contact items.</param>
public record GetContactsResponse(
    int Page,
    int PageSize,
    int TotalCount,
    int TotalPages,
    bool HasNextPage,
    bool HasPreviousPage,
    IReadOnlyCollection<ContactResponse> Items
);
