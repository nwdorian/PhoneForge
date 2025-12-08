using Domain.Contacts;

namespace TestData.Contacts;

public static class PhoneNumberData
{
    public static readonly PhoneNumber ValidPhoneNumber = PhoneNumber
        .Create("0919876543")
        .Value;

    public static readonly string LongerThanAllowedPhoneNumber = new(
        '*',
        PhoneNumber.MaxLength + 1
    );

    public static readonly string ShorterThanAllowedPhoneNumber = new(
        '*',
        PhoneNumber.MinLength - 1
    );

    public static readonly string InvalidFormatPhoneNumber = new("091-1234/567");
}
