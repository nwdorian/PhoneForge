namespace Console.Contacts.GenerateReport;

internal sealed record GenerateContactsReportRequest(
    string SearchTerm,
    string SortColumn,
    string SortOrder
);
