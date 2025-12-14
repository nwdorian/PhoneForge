namespace Console.Contacts.Get;

internal sealed record GetContactsResponse(
    int Page,
    int PageSize,
    int TotalCount,
    int TotalPages,
    bool HasNextPage,
    bool HasPreviousPage,
    IReadOnlyCollection<Contact> Items
);
