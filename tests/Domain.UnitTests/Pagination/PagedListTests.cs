using Domain.Core.Pagination;
using Domain.Core.Primitives;

namespace Domain.UnitTests.Pagination;

public class PagedListTests
{
    private static readonly List<int> _items = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11];
    private static readonly Result<Page> _firstPage = Page.Create(1);
    private static readonly Result<PageSize> _pageSize = PageSize.Create(10);
    private static readonly int _totalCount = _items.Count;

    [Fact]
    public void Constructor_Should_SetProperties()
    {
        PagedList<int> pagedList = new(
            _items,
            _firstPage.Value,
            _pageSize.Value,
            _totalCount
        );

        Assert.Equal(_firstPage.Value, pagedList.Page);
        Assert.Equal(_pageSize.Value, pagedList.PageSize);
        Assert.Equal(_totalCount, pagedList.TotalCount);

        Assert.Equal(_items.Count, pagedList.Items.Count);
        Assert.Equal(_items, pagedList.Items);
    }

    [Fact]
    public void HasNextPage_Should_ReturnTrue_WhenNextPageExists()
    {
        PagedList<int> pagedList = new(
            _items,
            _firstPage.Value,
            _pageSize.Value,
            _totalCount
        );

        Assert.True(pagedList.HasNextPage);
    }

    [Fact]
    public void HasNextPage_Should_ReturnFalse_WhenNextPageDoesNotExist()
    {
        Result<Page> lastPage = Page.Create(2);

        PagedList<int> pagedList = new(
            _items,
            lastPage.Value,
            _pageSize.Value,
            _totalCount
        );

        Assert.False(pagedList.HasNextPage);
    }

    [Fact]
    public void HasPreviousPage_Should_ReturnFalse_WhenOnFirstPage()
    {
        PagedList<int> pagedList = new(
            _items,
            _firstPage.Value,
            _pageSize.Value,
            _totalCount
        );

        Assert.False(pagedList.HasPreviousPage);
    }

    [Fact]
    public void HasPreviousPage_Should_ReturnTrue_WhenNotOnFirstPage()
    {
        Result<Page> secondPage = Page.Create(2);
        PagedList<int> pagedList = new(
            _items,
            secondPage.Value,
            _pageSize.Value,
            _totalCount
        );

        Assert.True(pagedList.HasPreviousPage);
    }
}
