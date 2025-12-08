using Domain.Core.Pagination;
using Domain.Core.Primitives;
using TestData.Pagination.Cases;

namespace UnitTests.Pagination;

public class PageSizeTests
{
    [Theory]
    [ClassData(typeof(PageSizeValid))]
    public void Create_Should_ReturnSuccess_WithValidInput(int input)
    {
        Result<PageSize> result = PageSize.Create(input);

        Assert.True(result.IsSuccess);
        Assert.Equal(input, result.Value);
    }

    [Theory]
    [ClassData(typeof(PageSizeInvalid))]
    public void Create_Should_ReturnError_WithInvalidInput(int input, Error expected)
    {
        Result<PageSize> result = PageSize.Create(input);

        Assert.True(result.IsFailure);
        Assert.Equal(expected, result.Error);
    }
}
