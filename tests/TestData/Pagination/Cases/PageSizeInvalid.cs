using Domain.Core.Pagination;
using Domain.Core.Primitives;
using Xunit;

namespace TestData.Pagination.Cases;

public class PageSizeInvalid : TheoryData<int, Error>
{
    public PageSizeInvalid()
    {
        Add(PageSizeData.GreaterThenAllowedPageSize, PaginationErrors.GreaterThanAllowed);
        Add(PageSizeData.LowerThenAllowedPageSize, PaginationErrors.LowerThanAllowed);
    }
}
