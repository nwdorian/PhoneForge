using SharedKernel;

namespace PhoneForge.Domain.Contacts;

/// <summary>
/// Represents the phone number value object.
/// </summary>
public sealed record PhoneNumber
{
    /// <summary>
    /// The phone number maximum length.
    /// </summary>
    public const int MaxLength = 20;

    /// <summary>
    /// The phone number minimum length.
    /// </summary>
    public const int MinLength = 6;

    /// <summary>
    /// Initializes a new instance of the <see cref="PhoneNumber"/> class.
    /// </summary>
    /// <param name="value">The phone number value.</param>
    private PhoneNumber(string value)
    {
        Value = value;
    }

    /// <summary>
    /// Gets the phone number value.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Implicitly converts a <see cref="PhoneNumber"/> instance to its underlying string value.
    /// </summary>
    /// <param name="phoneNumber">The <see cref="PhoneNumber"/> instance to convert.</param>
    /// <returns>The phone number as a string.</returns>
    public static implicit operator string(PhoneNumber phoneNumber)
    {
        return phoneNumber.Value;
    }

    /// <summary>
    /// Creates a new <see cref="PhoneNumber"/> instance based on the specified value.
    /// </summary>
    /// <param name="phoneNumber">The phone number value.</param>
    /// <returns>The result of the phone number creation process containing the phone number or an error.</returns>
    public static Result<PhoneNumber> Create(string? phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
        {
            return ContactErrors.PhoneNumber.NullOrEmpty;
        }

        if (phoneNumber.Length > MaxLength)
        {
            return ContactErrors.PhoneNumber.LongerThanAllowed;
        }

        if (phoneNumber.Length < MinLength)
        {
            return ContactErrors.PhoneNumber.ShorterThanAllowed;
        }

        if (!phoneNumber.All(char.IsDigit))
        {
            return ContactErrors.PhoneNumber.InvalidFormat;
        }

        return new PhoneNumber(phoneNumber);
    }
}
