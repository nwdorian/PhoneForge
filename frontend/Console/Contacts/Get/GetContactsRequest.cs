namespace Console.Contacts.Get;

internal sealed record GetContactsRequest(
    string SearchTerm,
    int Page,
    int PageSize,
    string SortColumn,
    string SortOrder
);
