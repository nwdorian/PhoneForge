using Xunit;

namespace TestData.Emails.Cases;

public class EmailValid : TheoryData<string>
{
    public EmailValid()
    {
        Add(EmailData.ValidEmail);
    }
}
