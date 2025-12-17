using Infrastructure.Database;
using IntegrationTests.Core.Abstractions;
using IntegrationTests.Core.Extensions;

namespace IntegrationTests.Contacts;

public class DeleteContactTests(IntegrationTestWebAppFactory factory)
    : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task Should_ReturnNoContent_WhenContactIsDeleted()
    {
        Guid contactId = DataSeeder.GetTestContact().Id;

        HttpResponseMessage response = await HttpClient.DeleteAsync(
            $"api/v1/contacts/{contactId}"
        );

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

        using PhoneForgeDbContext context = CreateDbContext();

        Contact? contact = await context
            .Contacts.IgnoreQueryFilters()
            .FirstOrDefaultAsync(c => c.Id == contactId);

        Assert.NotNull(contact);
        Assert.Equal(contact.Id, contactId);
        Assert.True(contact.Deleted);
        Assert.NotNull(contact.DeletedOnUtc);
    }

    [Fact]
    public async Task Should_ReturnNotFound_WhenContactDoesNotExist()
    {
        Guid contactId = Guid.NewGuid();
        Error expected = ContactErrors.NotFoundById(contactId);

        HttpResponseMessage response = await HttpClient.DeleteAsync(
            $"api/v1/contacts/{contactId}"
        );

        await response.AssertResponseErrorDetails(HttpStatusCode.NotFound, expected);
    }
}
