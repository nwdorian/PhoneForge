using System.Net;
using System.Net.Http.Json;
using Domain.Contacts;
using Domain.Core.Primitives;
using Infrastructure.Database;
using IntegrationTests.Core.Abstractions;
using IntegrationTests.Core.Extensions;
using TestData.Contacts.Update;
using WebApi.Contacts.Update;

namespace IntegrationTests.Contacts;

public class UpdateContactTests(IntegrationTestWebAppFactory factory)
    : BaseIntegrationTest(factory)
{
    [Theory]
    [ClassData(typeof(UpdateContactValidData))]
    public async Task Should_ReturnNoContent_WhenContactWasUpdated(
        UpdateContactRequest request
    )
    {
        Guid contactId = DataSeeder.GetTestContact().Id;

        HttpResponseMessage response = await HttpClient.PutAsJsonAsync(
            $"api/v1/contacts/{contactId}",
            request
        );

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

        using PhoneForgeDbContext context = CreateDbContext();

        Contact? contact = context.Contacts.FirstOrDefault(c => c.Id == contactId);

        Assert.NotNull(contact);
        Assert.Equal(request.FirstName, contact.FirstName);
        Assert.Equal(request.LastName, contact.LastName);
        Assert.Equal(request.Email, contact.Email);
        Assert.Equal(request.PhoneNumber, contact.PhoneNumber);
        Assert.NotNull(contact.ModifiedOnUtc);
    }

    [Theory]
    [ClassData(typeof(UpdateContactWithNonExistingIdData))]
    public async Task Should_ReturnNotFound_WhenContactDoesNotExist(
        UpdateContactRequest request,
        Guid contactId,
        Error expected
    )
    {
        HttpResponseMessage response = await HttpClient.PutAsJsonAsync(
            $"api/v1/contacts/{contactId}",
            request
        );

        await response.AssertResponseErrorDetails(HttpStatusCode.NotFound, expected);
    }

    [Theory]
    [ClassData(typeof(UpdateContactInvalidData))]
    public async Task Should_ReturnBadRequest_WhenRequestIsInvalid(
        UpdateContactRequest request,
        Error expected
    )
    {
        Guid contactId = DataSeeder.GetTestContact().Id;

        HttpResponseMessage response = await HttpClient.PutAsJsonAsync(
            $"api/v1/contacts/{contactId}",
            request
        );

        await response.AssertResponseErrorDetails(HttpStatusCode.BadRequest, expected);
    }

    [Theory]
    [ClassData(typeof(UpdateContactValidData))]
    public async Task Should_ReturnConflict_WhenEmailIsNotUnique(
        UpdateContactRequest request
    )
    {
        Error expected = ContactErrors.EmailNotUnique;
        Contact contact = DataSeeder.GetTestContact();
        UpdateContactRequest requestWithExistingEmail = request with
        {
            Email = contact.Email,
        };

        HttpResponseMessage response = await HttpClient.PutAsJsonAsync(
            $"api/v1/contacts/{contact.Id}",
            requestWithExistingEmail
        );

        await response.AssertResponseErrorDetails(HttpStatusCode.Conflict, expected);
    }
}
