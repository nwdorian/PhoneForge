using Domain.Contacts;
using Domain.Core.Primitives;

namespace UnitTests.Contacts;

public class FirstNameTests
{
    [Fact]
    public void Create_Should_ReturnSuccess_WithValidInput()
    {
        string input = "John";

        Result<FirstName> result = FirstName.Create(input);

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
        Result<FirstName> result = FirstName.Create(input);

        Assert.True(result.IsFailure);
        Assert.False(result.IsSuccess);
        Assert.Equal(ContactErrors.FirstName.NullOrEmpty, result.Error);
    }

    [Fact]
    public void Create_Should_ReturnError_WithTooLongInput()
    {
        string input = new('a', FirstName.MaxLength + 1);

        Result<FirstName> result = FirstName.Create(input);

        Assert.True(result.IsFailure);
        Assert.False(result.IsSuccess);
        Assert.Equal(ContactErrors.FirstName.LongerThanAllowed, result.Error);
    }
}
