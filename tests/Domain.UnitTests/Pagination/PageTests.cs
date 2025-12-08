using UnitTests.Pagination.Cases;

namespace UnitTests.Pagination;

public class PageTests
{
    [Theory]
    [ClassData(typeof(PageValid))]
    public void Create_Should_ReturnSuccess_WithValidInput(int input)
    {
        Result<Page> result = Page.Create(input);

        Assert.True(result.IsSuccess);
        Assert.Equal(input, result.Value);
    }

    [Theory]
    [ClassData(typeof(PageInvalid))]
    public void Create_Should_ReturnError_WithTooLowInput(int input, Error expected)
    {
        Result<Page> result = Page.Create(input);

        Assert.True(result.IsFailure);
        Assert.Equal(expected, result.Error);
    }
}
