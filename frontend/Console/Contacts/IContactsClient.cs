using Console.Contacts.Create;
using Console.Contacts.GenerateReport;
using Console.Contacts.Get;
using Console.Contacts.Update;
using Refit;

namespace Console.Contacts;

internal interface IContactsClient
{
    [Get("/contacts")]
    Task<ApiResponse<GetContactsResponse>> GetContacts(GetContactsRequest request);

    [Get("/contacts/report")]
    Task<IApiResponse> GenerateContactsReport(GenerateContactsReportRequest request);

    [Post("/contacts")]
    Task<IApiResponse> CreateContact([Body] CreateContactRequest request);

    [Delete("/contacts/{id}")]
    Task<IApiResponse> DeleteContact(Guid id);

    [Put("/contacts/{id}")]
    Task<IApiResponse> UpdateContact(Guid id, [Body] UpdateContactRequest request);
}
