using TestData.Contacts;

namespace UnitTests.Contacts.Cases.LastNames;

public class LastNameValid : TheoryData<string>
{
    public LastNameValid()
    {
        Add(LastNameData.ValidLastName);
    }
}
