using Xunit;

namespace TestData.Pagination.Cases;

public class PageSizeValid : TheoryData<int>
{
    public PageSizeValid()
    {
        Add(PageSizeData.MinimumPageSize);
    }
}
