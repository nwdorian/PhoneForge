using Xunit;

namespace TestData.PhoneNumbers.Cases;

public class PhoneNumberValid : TheoryData<string>
{
    public PhoneNumberValid()
    {
        Add(PhoneNumberData.ValidPhoneNumber);
    }
}
