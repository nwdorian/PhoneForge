using Domain.Contacts;

namespace TestData.Emails;

public static class EmailData
{
    public static readonly Email ValidEmail = Email.Create("test@phoneforge.com").Value;

    public static readonly string LongerThanAllowedEmail = new('*', Email.MaxLength + 1);

    public static readonly string InvalidFormatEmail = new("invalid-format-email");
}
