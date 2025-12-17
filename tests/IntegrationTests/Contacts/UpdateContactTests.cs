using Infrastructure.Database;
using IntegrationTests.Contacts.Cases.Update;
using IntegrationTests.Core.Abstractions;
using IntegrationTests.Core.Extensions;
using WebApi.Contacts.Update;

namespace IntegrationTests.Contacts;

public class UpdateContactTests(IntegrationTestWebAppFactory factory)
    : BaseIntegrationTest(factory)
{
    [Theory]
    [ClassData(typeof(UpdateContactValid))]
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

        Contact? contact = await context.Contacts.FirstOrDefaultAsync(c =>
            c.Id == contactId
        );

        Assert.NotNull(contact);
        Assert.Equal(request.FirstName, contact.FirstName);
        Assert.Equal(request.LastName, contact.LastName);
        Assert.Equal(request.Email, contact.Email);
        Assert.Equal(request.PhoneNumber, contact.PhoneNumber);
        Assert.NotNull(contact.ModifiedOnUtc);
    }

    [Theory]
    [ClassData(typeof(UpdateContactWithNonExistingId))]
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
    [ClassData(typeof(UpdateContactInvalid))]
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
    [ClassData(typeof(UpdateContactValid))]
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
