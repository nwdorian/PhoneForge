namespace Console.Contacts.Get;

internal sealed record GenerateContactsReportRequest(
    string SearchTerm,
    string SortColumn,
    string SortOrder
);
