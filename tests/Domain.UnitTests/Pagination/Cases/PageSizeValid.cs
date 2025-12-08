using TestData.Pagination;

namespace UnitTests.Pagination.Cases;

public class PageSizeValid : TheoryData<int>
{
    public PageSizeValid()
    {
        Add(PageSizeData.MinimumPageSize);
    }
}
