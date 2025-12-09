using TestData.Contacts;

namespace UnitTests.Contacts.Cases.PhoneNumbers;

public class PhoneNumberValid : TheoryData<string>
{
    public PhoneNumberValid()
    {
        Add(PhoneNumberData.ValidPhoneNumber);
    }
}
