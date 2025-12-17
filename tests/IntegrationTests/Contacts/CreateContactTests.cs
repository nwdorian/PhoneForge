using Application.Contacts;
using Infrastructure.Database;
using IntegrationTests.Contacts.Cases.Create;
using IntegrationTests.Core.Abstractions;
using IntegrationTests.Core.Extensions;
using WebApi.Contacts.Create;
using WebApi.Core.Constants;

namespace IntegrationTests.Contacts;

public class CreateContactTests(IntegrationTestWebAppFactory factory)
    : BaseIntegrationTest(factory)
{
    private const string CreateContactRoute = $"api/v1/{Routes.Contacts.Create}";

    [Theory]
    [ClassData(typeof(CreateContactValid))]
    public async Task Should_ReturnCreated_WhenContactIsCreated(
        CreateContactRequest request
    )
    {
        HttpResponseMessage response = await HttpClient.PostAsJsonAsync(
            CreateContactRoute,
            request
        );

        ContactResponse? result =
            await response.Content.ReadFromJsonAsync<ContactResponse>();

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.NotNull(result);

        using PhoneForgeDbContext context = CreateDbContext();

        Contact? contact = await context.Contacts.FirstOrDefaultAsync(c =>
            c.Id == result.Id
        );
        Assert.NotNull(contact);
    }

    [Theory]
    [ClassData(typeof(CreateContactInvalid))]
    public async Task Should_ReturnBadRequest_WhenRequestIsInvalid(
        CreateContactRequest request,
        Error expected
    )
    {
        HttpResponseMessage response = await HttpClient.PostAsJsonAsync(
            CreateContactRoute,
            request
        );

        await response.AssertResponseErrorDetails(HttpStatusCode.BadRequest, expected);
    }

    [Theory]
    [ClassData(typeof(CreateContactValid))]
    public async Task Should_ReturnConflict_WhenEmailIsNotUnique(
        CreateContactRequest request
    )
    {
        CreateContactRequest requestWithExistingEmail = request with
        {
            Email = DataSeeder.GetTestContact().Email,
        };

        Error expected = ContactErrors.EmailNotUnique;

        HttpResponseMessage response = await HttpClient.PostAsJsonAsync(
            CreateContactRoute,
            requestWithExistingEmail
        );

        await response.AssertResponseErrorDetails(HttpStatusCode.Conflict, expected);
    }
}
