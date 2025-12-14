using Console.Contacts.Get;
using Refit;

namespace Console.Contacts;

internal interface IContactsClient
{
    [Get("/contacts")]
    Task<ApiResponse<Response>> GetContacts(Request? request);
}
