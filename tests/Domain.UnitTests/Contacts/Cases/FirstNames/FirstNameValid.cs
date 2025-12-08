using TestData.FirstNames;

namespace UnitTests.Contacts.Cases.FirstNames;

public class FirstNameValid : TheoryData<string>
{
    public FirstNameValid()
    {
        Add(FirstNameData.ValidFirstName);
    }
}
