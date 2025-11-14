using SharedKernel;

namespace PhoneForge.Domain.Contacts;

/// <summary>
/// Represents the email value object.
/// </summary>
public sealed record Email
{
    /// <summary>
    /// The email maximum length.
    /// </summary>
    public const int MaxLength = 100;

    /// <summary>
    /// Initializes a new instance of the <see cref="Email"/> class.
    /// </summary>
    /// <param name="value">The email value.</param>
    private Email(string value)
    {
        Value = value;
    }

    /// <summary>
    /// Gets the email value.
    /// </summary>
    public string Value { get; }

    public static implicit operator string(Email email)
    {
        return email.Value;
    }

    /// <summary>
    /// Creates a new <see cref="Email"/> instance based on the specified value.
    /// </summary>
    /// <param name="email">The email value.</param>
    /// <returns>The result of the email creation process containing the email or an error.</returns>
    public static Result<Email> Create(string? email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return ContactErrors.Email.NullOrEmpty;
        }

        if (email.Length > MaxLength)
        {
            return ContactErrors.Email.LongerThanAllowed;
        }

        if (!email.Contains('@'))
        {
            return ContactErrors.Email.InvalidFormat;
        }

        return new Email(email);
    }
}
