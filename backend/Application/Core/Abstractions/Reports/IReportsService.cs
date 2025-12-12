using Application.Contacts;

namespace Application.Core.Abstractions.Reports;

/// <summary>
/// Represents the reports service interface.
/// </summary>
public interface IReportsService
{
    /// <summary>
    /// Gets the contacts report in HTML format.
    /// </summary>
    /// <param name="contacts">The contacts collection.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task GenerateContactsReport(List<ContactResponse> contacts);
}
