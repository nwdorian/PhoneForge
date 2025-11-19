using Domain.Core.Primitives;

namespace Domain.Contacts;

/// <summary>
/// Represents the first name value object.
/// </summary>
public sealed record FirstName
{
    /// <summary>
    /// The first name maximum length.
    /// </summary>
    public const int MaxLength = 100;

    /// <summary>
    /// Initializes a new instance of the <see cref="FirstName"/> class.
    /// </summary>
    /// <param name="value">The first name value.</param>
    private FirstName(string value)
    {
        Value = value;
    }

    /// <summary>
    /// Gets the first name value.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Implicitly converts a <see cref="FirstName"/> instance to its underlying string value.
    /// </summary>
    /// <param name="firstName">The <see cref="FirstName"/> instance to convert.</param>
    /// <returns>The first name as a string.</returns>
    public static implicit operator string(FirstName firstName)
    {
        return firstName.Value;
    }

    /// <summary>
    /// Creates a new <see cref="FirstName"/> instance based on the specified value.
    /// </summary>
    /// <param name="firstName">The first name value.</param>
    /// <returns>The result of the first name creation process containing the first name or an error.</returns>
    public static Result<FirstName> Create(string? firstName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
        {
            return ContactErrors.FirstName.NullOrEmpty;
        }

        if (firstName.Length > MaxLength)
        {
            return ContactErrors.FirstName.LongerThanAllowed;
        }

        return new FirstName(firstName);
    }
}
