using Domain.Core.Primitives;

namespace Domain.Contacts;

/// <summary>
/// Represents the last name value object.
/// </summary>
public sealed record LastName
{
    /// <summary>
    /// The last name maximum length.
    /// </summary>
    public const int MaxLength = 100;

    /// <summary>
    ///Initializes a new instance of the <see cref="LastName"/> class.
    /// </summary>
    /// <param name="value">The last name value.</param>
    private LastName(string value)
    {
        Value = value;
    }

    /// <summary>
    /// Gets the last name value.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Implicitly converts a <see cref="LastName"/> instance to its underlying string value.
    /// </summary>
    /// <param name="lastName">The <see cref="LastName"/> instance to convert.</param>
    /// <returns>The last name as a string.</returns>
    public static implicit operator string(LastName lastName)
    {
        return lastName.Value;
    }

    /// <summary>
    /// Creates a new <see cref="LastName"/> instance based on the specified value.
    /// </summary>
    /// <param name="lastName">The last name value.</param>
    /// <returns>The result of the last name creation process containing the last name or an error.</returns>
    public static Result<LastName> Create(string? lastName)
    {
        if (string.IsNullOrWhiteSpace(lastName))
        {
            return ContactErrors.LastName.NullOrEmpty;
        }

        if (lastName.Length > MaxLength)
        {
            return ContactErrors.LastName.LongerThanAllowed;
        }

        return new LastName(lastName);
    }
}
