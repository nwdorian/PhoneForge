using TestData.Emails;
using TestData.FirstNames;
using TestData.LastNames;
using TestData.PhoneNumbers;
using WebApi.Contacts.Update;

namespace TestData.Contacts.Update;

public static class UpdateContactRequestData
{
    public static UpdateContactRequest CreateValidRequest()
    {
        return new UpdateContactRequest(
            FirstNameData.ValidFirstName,
            LastNameData.ValidLastName,
            EmailData.ValidEmail,
            PhoneNumberData.ValidPhoneNumber
        );
    }

    public static UpdateContactRequest CreateRequestWithInvalidFirstName()
    {
        return new UpdateContactRequest(
            FirstNameData.LongerThanAllowedFirstName,
            LastNameData.ValidLastName,
            EmailData.ValidEmail,
            PhoneNumberData.ValidPhoneNumber
        );
    }

    public static UpdateContactRequest CreateRequestWithInvalidLastName()
    {
        return new UpdateContactRequest(
            FirstNameData.ValidFirstName,
            LastNameData.LongerThanAllowedLastName,
            EmailData.ValidEmail,
            PhoneNumberData.ValidPhoneNumber
        );
    }

    public static UpdateContactRequest CreateRequestWithInvalidEmail()
    {
        return new UpdateContactRequest(
            FirstNameData.ValidFirstName,
            LastNameData.ValidLastName,
            EmailData.LongerThanAllowedEmail,
            PhoneNumberData.ValidPhoneNumber
        );
    }

    public static UpdateContactRequest CreateRequestWithInvalidPhoneNumber()
    {
        return new UpdateContactRequest(
            FirstNameData.ValidFirstName,
            LastNameData.ValidLastName,
            EmailData.ValidEmail,
            PhoneNumberData.LongerThanAllowedPhoneNumber
        );
    }
}
