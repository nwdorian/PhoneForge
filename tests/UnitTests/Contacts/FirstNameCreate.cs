using Domain.Contacts;
using Domain.Core.Primitives;

namespace UnitTests.Contacts;

public class FirstNameCreate
{
    [Fact]
    public void FirstNameCreate_WithValidValue_ShouldReturnSuccess()
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
    public void FirstNameCreate_WithNullOrEmpty_ShouldReturnFailure(string? input)
    {
        Result<FirstName> result = FirstName.Create(input);

        Assert.True(result.IsFailure);
        Assert.False(result.IsSuccess);
        Assert.Equal(ContactErrors.FirstName.NullOrEmpty, result.Error);
    }

    [Fact]
    public void FirstNameCreate_WithTooLongInput_ShouldReturnFailure()
    {
        string input = new('a', FirstName.MaxLength + 1);

        Result<FirstName> result = FirstName.Create(input);

        Assert.True(result.IsFailure);
        Assert.False(result.IsSuccess);
        Assert.Equal(ContactErrors.FirstName.LongerThanAllowed, result.Error);
    }

    [Fact]
    public void ImplicitConversion_Should_ReturnValueAsString()
    {
        string input = "John";
        Result<FirstName> result = FirstName.Create(input);

        string converted = result.Value;

        Assert.Equal(input, converted);
    }
}
