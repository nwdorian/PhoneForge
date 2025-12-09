using TestData.Pagination;

namespace UnitTests.Pagination.Cases;

public class PageInvalid : TheoryData<int, Error>
{
    public PageInvalid()
    {
        Add(PageData.InvalidPage, PaginationErrors.InvalidPage);
    }
}
