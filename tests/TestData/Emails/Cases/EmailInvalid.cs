using Domain.Contacts;
using Domain.Core.Primitives;
using Xunit;

namespace TestData.Emails.Cases;

public class EmailInvalid : TheoryData<string?, Error>
{
    public EmailInvalid()
    {
        Add(null, ContactErrors.Email.NullOrEmpty);
        Add(string.Empty, ContactErrors.Email.NullOrEmpty);
        Add(" ", ContactErrors.Email.NullOrEmpty);
        Add(EmailData.LongerThanAllowedEmail, ContactErrors.Email.LongerThanAllowed);
        Add(EmailData.InvalidFormatEmail, ContactErrors.Email.InvalidFormat);
    }
}
