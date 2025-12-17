using Application.Contacts.Get;
using Infrastructure.Database;
using IntegrationTests.Contacts.Cases.Get;
using IntegrationTests.Core.Abstractions;
using IntegrationTests.Core.Extensions;
using WebApi.Contacts.Get;
using WebApi.Core.Constants;

namespace IntegrationTests.Contacts;

public class GetContactsTests(IntegrationTestWebAppFactory factory)
    : BaseIntegrationTest(factory)
{
    private const string GetContactsRoute = $"api/v1/{Routes.Contacts.Get}";

    [Theory]
    [ClassData(typeof(GetContactsValid))]
    public async Task Should_ReturnCorrectPages(
        GetContactsRequest request,
        int totalCount,
        int totalPages,
        bool hasPreviousPage,
        bool hasNextPage
    )
    {
        GetContactsResponse? response =
            await HttpClient.GetFromJsonAsync<GetContactsResponse>(
                $"{GetContactsRoute}?page={request.Page}"
            );

        Assert.NotNull(response);
        Assert.Equal(totalCount, response.TotalCount);
        Assert.Equal(totalPages, response.TotalPages);
        Assert.Equal(request.Page, response.Page);
        Assert.Equal(request.PageSize, response.PageSize);
        Assert.Equal(hasPreviousPage, response.HasPreviousPage);
        Assert.Equal(hasNextPage, response.HasNextPage);

        using PhoneForgeDbContext context = CreateDbContext();

        List<Guid> contacts = await context
            .Contacts.OrderBy(c => c.CreatedOnUtc)
            .Select(c => c.Id)
            .Take(10)
            .ToListAsync();

        Assert.Equal(response.Items.Count, contacts.Count);
    }

    [Fact]
    public async Task Should_ReturnContacts_WithSearchTerm()
    {
        string email = DataSeeder.GetTestContact().Email;

        GetContactsResponse? response =
            await HttpClient.GetFromJsonAsync<GetContactsResponse>(
                $"{GetContactsRoute}?searchTerm={email}"
            );

        Assert.NotNull(response);

        using PhoneForgeDbContext context = CreateDbContext();

        List<Contact> contacts = await context
            .Contacts.Where(x => x.Email.Value.Contains(email))
            .ToListAsync();

        Assert.Equal(contacts.Count, response.Items.Count);
    }

    [Theory]
    [ClassData(typeof(GetContactsInvalid))]
    public async Task Should_ReturnError_WhenPagingDataIsInvalid(
        GetContactsRequest request,
        Error expected
    )
    {
        HttpResponseMessage? response = await HttpClient.GetAsync(
            $"{GetContactsRoute}?page={request.Page}&pageSize={request.PageSize}"
        );

        await response.AssertResponseErrorDetails(HttpStatusCode.BadRequest, expected);
    }
}
