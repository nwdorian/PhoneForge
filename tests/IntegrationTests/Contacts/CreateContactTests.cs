using System.Net;
using System.Net.Http.Json;
using Application.Contacts;
using Domain.Contacts;
using Domain.Core.Primitives;
using IntegrationTests.Core.Abstractions;
using IntegrationTests.Core.Contracts;
using IntegrationTests.Core.Extensions;
using TestData.Contacts.CreateContact;
using WebApi.Contacts.Create;
using WebApi.Core.Constants;

namespace IntegrationTests.Contacts;

public class CreateContactTests(IntegrationTestWebAppFactory factory)
    : BaseIntegrationTest(factory)
{
    private const string CreateContactRoute = $"api/v1/{Routes.Contacts.Create}";

    [Theory]
    [ClassData(typeof(CreateContactValidData))]
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

        Contact? contact = DbContext.Contacts.FirstOrDefault(c => c.Id == result.Id);
        Assert.NotNull(contact);
    }

    [Theory]
    [ClassData(typeof(CreateContactInvalidData))]
    public async Task Should_ReturnBadRequest_WhenRequestIsInvalid(
        CreateContactRequest request,
        Error expected
    )
    {
        HttpResponseMessage response = await HttpClient.PostAsJsonAsync(
            CreateContactRoute,
            request
        );

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        CustomProblemDetails? problemDetails = await response.GetProblemDetails();

        Assert.NotNull(problemDetails);
        Assert.Equal(expected.Description, problemDetails.Errors[0]);
        Assert.Equal(expected.Code, problemDetails.Title);
    }

    [Theory]
    [ClassData(typeof(CreateContactValidData))]
    public async Task Should_ReturnConflict_WhenEmailIsNotUnique(
        CreateContactRequest request
    )
    {
        CreateContactRequest requestWithExistingEmail = request with
        {
            Email = DataSeeder.GetTestContact().Email,
        };

        HttpResponseMessage response = await HttpClient.PostAsJsonAsync(
            CreateContactRoute,
            requestWithExistingEmail
        );

        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
    }
}
