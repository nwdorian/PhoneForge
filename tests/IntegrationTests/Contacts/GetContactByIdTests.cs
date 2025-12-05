using System.Net;
using System.Net.Http.Json;
using Application.Contacts;
using Domain.Contacts;
using Domain.Core.Primitives;
using IntegrationTests.Core.Abstractions;
using IntegrationTests.Core.Contracts;
using IntegrationTests.Core.Extensions;
using WebApi.Core.Constants;

namespace IntegrationTests.Contacts;

public class GetContactByIdTests(IntegrationTestWebAppFactory factory)
    : BaseIntegrationTest(factory)
{
    private const string GetByIdRoute = $"api/v1/{Routes.Contacts.Get}";

    [Fact]
    public async Task Should_ReturnOk_And_Contact_WhenContactDoesExist()
    {
        Guid contactId = DataSeeder.GetTestContact().Id;

        ContactResponse? response = await HttpClient.GetFromJsonAsync<ContactResponse>(
            $"{GetByIdRoute}/{contactId}"
        );

        Assert.NotNull(response);
    }

    [Fact]
    public async Task Should_ReturnNotFound_WhenContactDoesNotExist()
    {
        Guid contactId = Guid.NewGuid();
        Error expected = ContactErrors.NotFoundById(contactId);

        HttpResponseMessage response = await HttpClient.GetAsync(
            $"{GetByIdRoute}/{contactId}"
        );

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

        CustomProblemDetails? problemDetails = await response.GetProblemDetails();

        Assert.NotNull(problemDetails);
        Assert.Equal(expected.Description, problemDetails.Errors[0]);
        Assert.Equal(expected.Code, problemDetails.Title);
    }
}
