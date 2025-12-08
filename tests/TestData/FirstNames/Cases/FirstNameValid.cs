using Xunit;

namespace TestData.FirstNames.Cases;

public class FirstNameValid : TheoryData<string>
{
    public FirstNameValid()
    {
        Add(FirstNameData.ValidFirstName);
    }
}
