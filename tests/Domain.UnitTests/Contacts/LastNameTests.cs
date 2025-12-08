using UnitTests.Contacts.Cases.LastNames;

namespace UnitTests.Contacts;

public class LastNameTests
{
    [Theory]
    [ClassData(typeof(LastNameValid))]
    public void Create_Should_ReturnSuccess_WithValidInput(string input)
    {
        Result<LastName> result = LastName.Create(input);

        Assert.True(result.IsSuccess);
        Assert.Equal(input, result.Value);
    }

    [Theory]
    [ClassData(typeof(LastNameInvalid))]
    public void Create_Should_ReturnError_WithInvalidInput(string? input, Error expected)
    {
        Result<LastName> result = LastName.Create(input);

        Assert.True(result.IsFailure);
        Assert.Equal(expected, result.Error);
    }
}
