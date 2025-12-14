using Console.Contacts.Get;

namespace Console.Contacts.Abstractions;

internal interface IContactsService
{
    Task<GetContactsResponse?> GetContacts(GetContactsRequest request);

    Task GenerateReport(GenerateContactsReportRequest request);
}
