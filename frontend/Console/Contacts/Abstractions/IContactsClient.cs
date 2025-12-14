using Console.Contacts.Get;
using Refit;

namespace Console.Contacts.Abstractions;

internal interface IContactsClient
{
    [Get("/contacts")]
    Task<ApiResponse<GetContactsResponse>> GetContacts(GetContactsRequest request);

    [Get("/contacts/report")]
    Task<IApiResponse> GenerateContactsReport(GenerateContactsReportRequest request);
}
