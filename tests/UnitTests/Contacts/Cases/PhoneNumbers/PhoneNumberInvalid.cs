using TestData.Contacts;

namespace UnitTests.Contacts.Cases.PhoneNumbers;

public class PhoneNumberInvalid : TheoryData<string?, Error>
{
    public PhoneNumberInvalid()
    {
        Add(null, ContactErrors.PhoneNumber.NullOrEmpty);
        Add(string.Empty, ContactErrors.PhoneNumber.NullOrEmpty);
        Add(" ", ContactErrors.PhoneNumber.NullOrEmpty);
        Add(
            PhoneNumberData.LongerThanAllowedPhoneNumber,
            ContactErrors.PhoneNumber.LongerThanAllowed
        );
        Add(
            PhoneNumberData.ShorterThanAllowedPhoneNumber,
            ContactErrors.PhoneNumber.ShorterThanAllowed
        );
        Add(
            PhoneNumberData.InvalidFormatPhoneNumber,
            ContactErrors.PhoneNumber.InvalidFormat
        );
    }
}
