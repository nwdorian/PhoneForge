using Domain.Contacts;
using Domain.Core.Primitives;

namespace UnitTests.Domain.Contacts;

public class LastNameTests
{
    [Fact]
    public void Create_Should_ReturnSuccess_WithValidInput()
    {
        string input = "Doe";

        Result<LastName> result = LastName.Create(input);

        Assert.True(result.IsSuccess);
        Assert.False(result.IsFailure);
        Assert.Equal(input, result.Value);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Create_Should_ReturnError_WithNullOrEmptyInput(string? input)
    {
        Result<LastName> result = LastName.Create(input);

        Assert.True(result.IsFailure);
        Assert.False(result.IsSuccess);
        Assert.Equal(ContactErrors.LastName.NullOrEmpty, result.Error);
    }

    [Fact]
    public void Create_Should_ReturnError_WithTooLongInput()
    {
        string input = new('a', LastName.MaxLength + 1);

        Result<LastName> result = LastName.Create(input);

        Assert.True(result.IsFailure);
        Assert.False(result.IsSuccess);
        Assert.Equal(ContactErrors.LastName.LongerThanAllowed, result.Error);
    }
}
