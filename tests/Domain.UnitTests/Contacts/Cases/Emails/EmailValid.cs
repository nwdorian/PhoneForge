using TestData.Contacts;

namespace UnitTests.Contacts.Cases.Emails;

public class EmailValid : TheoryData<string>
{
    public EmailValid()
    {
        Add(EmailData.ValidEmail);
    }
}
