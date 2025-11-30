using Domain.Core.Pagination;
using Domain.Core.Primitives;

namespace UnitTests.Domain.Pagination;

public class PageSizeTests
{
    [Fact]
    public void Create_Should_ReturnSuccess_WithValidInput()
    {
        int input = 10;

        Result<PageSize> result = PageSize.Create(input);

        Assert.True(result.IsSuccess);
        Assert.False(result.IsFailure);
        Assert.Equal(input, result.Value);
    }

    [Fact]
    public void Create_Should_ReturnError_WithTooLowInput()
    {
        int input = PageSize.MinimumPageSize - 1;

        Result<PageSize> result = PageSize.Create(input);

        Assert.True(result.IsFailure);
        Assert.False(result.IsSuccess);
        Assert.Equal(PaginationErrors.LowerThanAllowed, result.Error);
    }

    [Fact]
    public void Create_Should_ReturnError_WithTooHighInput()
    {
        int input = PageSize.MaximumPageSize + 1;

        Result<PageSize> result = PageSize.Create(input);

        Assert.True(result.IsFailure);
        Assert.False(result.IsSuccess);
        Assert.Equal(PaginationErrors.GreaterThanAllowed, result.Error);
    }
}
