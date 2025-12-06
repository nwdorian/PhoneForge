using System.Net.Http.Json;
using Application.Contacts;
using Domain.Core.Pagination;
using Infrastructure.Database;
using IntegrationTests.Core.Abstractions;
using WebApi.Core.Constants;

namespace IntegrationTests.Contacts;

public class GetContactsTests(IntegrationTestWebAppFactory factory)
    : BaseIntegrationTest(factory)
{
    private const string GetContactsRoute = $"api/v1/{Routes.Contacts.Get}";

    [Fact]
    public async Task Should_ReturnFirstPage()
    {
        PagedList<ContactResponse>? response = await HttpClient.GetFromJsonAsync<
            PagedList<ContactResponse>
        >(GetContactsRoute);

        Assert.NotNull(response);
        Assert.Equal(20, response.TotalCount);
        Assert.Equal(1, response.Page);
        Assert.Equal(10, response.PageSize);
        Assert.False(response.HasPreviousPage);
        Assert.True(response.HasNextPage);

        using PhoneForgeDbContext context = CreateDbContext();

        List<Guid> contacts = context
            .Contacts.OrderBy(c => c.CreatedOnUtc)
            .Select(c => c.Id)
            .Take(10)
            .ToList();

        Assert.Equal(response.Items.Count, contacts.Count);
    }
}
