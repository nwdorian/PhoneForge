using SharedKernel;

namespace PhoneForge.Domain.Contacts;

/// <summary>
/// Contains the contact errors.
/// </summary>
public static class ContactErrors
{
    /// <summary>
    /// Gets an error indicating that the email is not unique.
    /// </summary>
    public static Error EmailNotUnique =>
        Error.Conflict("Contact.EmailNotUnique", "The provided email is not unique.");

    /// <summary>
    /// Contains the first name errors.
    /// </summary>
    public static class FirstName
    {
        /// <summary>
        /// Gets an error indicating that the first name is null or empty.
        /// </summary>
        public static Error NullOrEmpty =>
            Error.Validation("FirstName.NullOrEmpty", "The first name is required.");

        /// <summary>
        /// Gets an error indicating that the first name exceeds the maximum allowed length.
        /// </summary>
        public static Error LongerThanAllowed =>
            Error.Validation(
                "FirstName.LongerThanAllowed",
                "The first name is longer than allowed"
            );

        /// <summary>
        /// Gets an error indicating that the first name is required.
        /// </summary>
        public static Error IsRequired =>
            Error.Validation("FirstName.IsRequired", "The first name is required.");
    }

    /// <summary>
    /// Contains the last name errors.
    /// </summary>
    public static class LastName
    {
        /// <summary>
        /// Gets an error indicating that the last name is null or empty.
        /// </summary>
        public static Error NullOrEmpty =>
            Error.Validation("LastName.NullOrEmpty", "The last name is required.");

        /// <summary>
        /// Gets an error indicating that the last name exceeds the maximum allowed length.
        /// </summary>
        public static Error LongerThanAllowed =>
            Error.Validation(
                "LastName.LongerThanAllowed",
                "The last name is longer than allowed."
            );

        /// <summary>
        /// Gets an error indicating that the last name is required.
        /// </summary>
        public static Error IsRequired =>
            Error.Validation("LastName.IsRequired", "The last name is required.");
    }

    /// <summary>
    /// Contains the email errors.
    /// </summary>
    public static class Email
    {
        /// <summary>
        /// Gets an error indicating that the email is null or empty.
        /// </summary>
        public static Error NullOrEmpty =>
            Error.Validation("Email.NullOrEmpty", "The email is required.");

        /// <summary>
        /// Gets an error indicating that the email exceeds the maximum allowed length.
        /// </summary>
        public static Error LongerThanAllowed =>
            Error.Validation(
                "Email.LongerThanAllowed",
                "The email is longer than allowed."
            );

        /// <summary>
        /// Gets an error indicating that the email format is invalid.
        /// </summary>
        public static Error InvalidFormat =>
            Error.Validation("Email.InvalidFormat", "The email format is invalid.");

        /// <summary>
        /// Gets an error indicating that the email is required.
        /// </summary>
        public static Error IsRequired =>
            Error.Validation("Email.IsRequired", "The email is required.");
    }

    /// <summary>
    /// Contains the phone number errors.
    /// </summary>
    public static class PhoneNumber
    {
        /// <summary>
        /// Gets an error indicating that the phone number is null or empty.
        /// </summary>
        public static Error NullOrEmpty =>
            Error.Validation("PhoneNumber.NullOrEmpty", "The phone number is required.");

        /// <summary>
        /// Gets an error indicating that the phone number exceeds the maximum allowed length.
        /// </summary>
        public static Error LongerThanAllowed =>
            Error.Validation(
                "PhoneNumber.LongerThanAllowed",
                "The phone number is longer than allowed"
            );

        /// <summary>
        /// Gets an error indicating that the phone number is shorter than the minimum allowed length.
        /// </summary>
        public static Error ShorterThanAllowed =>
            Error.Validation(
                "PhoneNumber.ShorterThanAllowed",
                "The phone number is shorter than allowed."
            );

        /// <summary>
        /// Gets an error indicating that the phone number format is invalid.
        /// </summary>
        public static Error InvalidFormat =>
            Error.Validation(
                "PhoneNumber.InvalidFormat",
                "The phone number format is invalid."
            );

        /// <summary>
        /// Gets an error indicating that the phone number is required.
        /// </summary>
        public static Error IsRequired =>
            Error.Validation("PhoneNumber.IsRequired", "The phone number is required.");
    }
}
