using Domain.Core.Pagination;
using TestData.Pagination;

namespace UnitTests.Pagination.Cases;

public class PagedListValid : TheoryData<List<int>, Page, PageSize, int, int, bool, bool>
{
    public PagedListValid()
    {
        Add(
            PagedListData.Items,
            PageData.FirstPage,
            PageSizeData.MinimumPageSize,
            PagedListData.Items.Count,
            2,
            true,
            false
        );

        Add(
            PagedListData.Items,
            PageData.SecondPage,
            PageSizeData.MinimumPageSize,
            PagedListData.Items.Count,
            2,
            false,
            true
        );
    }
}
