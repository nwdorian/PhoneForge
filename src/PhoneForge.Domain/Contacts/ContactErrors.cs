using SharedKernel;

namespace PhoneForge.Domain.Contacts;

/// <summary>
/// Contains the contact errors.
/// </summary>
public static class ContactErrors
{
    /// <summary>
    /// Contains the first name errors.
    /// </summary>
    public static class FirstName
    {
        public static Error NullOrEmpty => Error.Validation("FirstName.NullOrEmpty", "The first name is required.");

        public static Error LongerThanAllowed =>
            Error.Validation("FirstName.LongerThanAllowed", "The first name is longer than allowed");
    }

    /// <summary>
    /// Contains the last name errors.
    /// </summary>
    public static class LastName
    {
        public static Error NullOrEmpty => Error.Validation("LastName.NullOrEmpty", "The last name is required.");

        public static Error LongerThanAllowed =>
            Error.Validation("LastName.LongerThanAllowed", "The last name is longer than allowed.");
    }

    /// <summary>
    /// Contains the email errors.
    /// </summary>
    public static class Email
    {
        public static Error NullOrEmpty => Error.Validation("Email.NullOrEmpty", "The email is required.");

        public static Error LongerThanAllowed =>
            Error.Validation("Email.LongerThanAllowed", "The email is longer than allowed.");

        public static Error InvalidFormat => Error.Validation("Email.InvalidFormat", "The email format is invalid.");
    }

    /// <summary>
    /// Contains the phone number errors.
    /// </summary>
    public static class PhoneNumber
    {
        public static Error NullOrEmpty => Error.Validation("PhoneNumber.NullOrEmpty", "The phone number is required.");

        public static Error LongerThanAllowed =>
            Error.Validation("PhoneNumber.LongerThanAllowed", "The phone number is longer than allowed");

        public static Error ShorterThanAllowed =>
            Error.Validation("PhoneNumber.ShorterThanAllowed", "The phone number is shorter than allowed.");

        public static Error InvalidFormat =>
            Error.Validation("PhoneNumber.InvalidFormat", "The phone number format is invalid.");
    }
}
