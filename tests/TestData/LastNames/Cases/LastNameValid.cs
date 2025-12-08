using Xunit;

namespace TestData.LastNames.Cases;

public class LastNameValid : TheoryData<string>
{
    public LastNameValid()
    {
        Add(LastNameData.ValidLastName);
    }
}
