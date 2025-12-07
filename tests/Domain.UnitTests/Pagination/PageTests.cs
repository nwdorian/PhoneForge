using Domain.Core.Pagination;
using Domain.Core.Primitives;

namespace UnitTests.Pagination;

public class PageTests
{
    [Fact]
    public void Create_Should_ReturnSuccess_WithValidInput()
    {
        int input = 1;

        Result<Page> result = Page.Create(input);

        Assert.True(result.IsSuccess);
        Assert.False(result.IsFailure);
        Assert.Equal(input, result.Value);
    }

    [Fact]
    public void Create_Should_ReturnError_WithTooLowInput()
    {
        int input = 0;

        Result<Page> result = Page.Create(input);

        Assert.True(result.IsFailure);
        Assert.False(result.IsSuccess);
        Assert.Equal(PaginationErrors.InvalidPage, result.Error);
    }
}
