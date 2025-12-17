using WebApi.Core.Constants;

namespace WebApi.Contacts.GenerateReport;

/// <summary>
/// Represents the request for generating a contacts report.
/// </summary>
/// <param name="SearchTerm">The search term.</param>
/// <param name="SortColumn">The sorting column.</param>
/// <param name="SortOrder">The sorting order.</param>
public sealed record GenerateContactsReportRequest(
    string? SearchTerm,
    string SortColumn = Pagination.DefaultSortColum,
    string SortOrder = Pagination.DefaultSortOrder
);
