using Application.Core.Abstractions.Messaging;

namespace Application.Contacts.GenerateReport;

/// <summary>
/// The command for generating a pdf report.
/// </summary>
/// <param name="SearchTerm">The search term.</param>
/// <param name="SortColumn">The sorting column.</param>
/// <param name="SortOrder">The sorting order.</param>
public record GenerateContactsReportCommand(
    string? SearchTerm,
    string SortColumn,
    string SortOrder
) : ICommand;
