using Domain.Core.Pagination;
using UnitTests.Pagination.Cases;

namespace UnitTests.Pagination;

public class PagedListTests
{
    [Theory]
    [ClassData(typeof(PagedListValid))]
    public void Constructor_Should_SetProperties(
        List<int> items,
        Page page,
        PageSize pageSize,
        int totalCount,
        int totalPages,
        bool hasNextPage,
        bool hasPreviousPage
    )
    {
        PagedList<int> pagedList = new(items, page, pageSize, totalCount);

        Assert.Equal(items, pagedList.Items);
        Assert.Equal(page, pagedList.Page);
        Assert.Equal(pageSize, pagedList.PageSize);
        Assert.Equal(items.Count, pagedList.TotalCount);
        Assert.Equal(totalPages, pagedList.TotalPages);
        Assert.Equal(hasNextPage, pagedList.HasNextPage);
        Assert.Equal(hasPreviousPage, pagedList.HasPreviousPage);
    }
}
