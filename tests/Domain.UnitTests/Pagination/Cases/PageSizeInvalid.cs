using Domain.Core.Pagination;
using Domain.Core.Primitives;
using TestData.Pagination;

namespace UnitTests.Pagination.Cases;

public class PageSizeInvalid : TheoryData<int, Error>
{
    public PageSizeInvalid()
    {
        Add(PageSizeData.GreaterThenAllowedPageSize, PaginationErrors.GreaterThanAllowed);
        Add(PageSizeData.LowerThenAllowedPageSize, PaginationErrors.LowerThanAllowed);
    }
}
