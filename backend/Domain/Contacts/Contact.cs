using Domain.Core;
using Domain.Core.Abstractions;
using Domain.Core.Primitives;

namespace Domain.Contacts;

/// <summary>
/// Represents the user entity.
/// </summary>
public sealed class Contact : Entity, ISoftDeletableEntity, IAuditableEntity
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Contact"/> class.
    /// </summary>
    /// <param name="firstName">The contact first name.</param>
    /// <param name="lastName">The contact last name.</param>
    /// <param name="email">The contact email.</param>
    /// <param name="phoneNumber">The contact phone number.</param>
    private Contact(
        FirstName firstName,
        LastName lastName,
        Email email,
        PhoneNumber phoneNumber
    )
        : base(Guid.NewGuid())
    {
        Ensure.NotNull(firstName, "The first name is required", nameof(firstName));
        Ensure.NotNull(lastName, "The last name is required", nameof(lastName));
        Ensure.NotNull(email, "The email is required", nameof(email));
        Ensure.NotNull(phoneNumber, "The phone number is required", nameof(phoneNumber));

        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Contact"/> class.
    /// </summary>
    /// <remarks>
    /// Required by EF Core.
    /// </remarks>
#pragma warning disable CS8618
    private Contact() { }
#pragma warning restore CS8618

    /// <summary>
    /// Gets or sets the contact first name.
    /// </summary>
    public FirstName FirstName { get; private set; }

    /// <summary>
    /// Gets or sets the contact last name.
    /// </summary>
    public LastName LastName { get; private set; }

    /// <summary>
    /// Gets the contact full name.
    /// </summary>
    public string FullName => $"{FirstName.Value} {LastName.Value}";

    /// <summary>
    /// Gets or sets the contact email.
    /// </summary>
    public Email Email { get; private set; }

    /// <summary>
    /// Gets or sets the contact phone number.
    /// </summary>
    public PhoneNumber PhoneNumber { get; private set; }

    /// <inheritdoc />
    public DateTime? DeletedOnUtc { get; }

    /// <inheritdoc />
    public bool Deleted { get; }

    /// <inheritdoc />
    public DateTime CreatedOnUtc { get; }

    /// <inheritdoc />
    public DateTime? ModifiedOnUtc { get; }

    /// <summary>
    /// Creates a new contact with the specified first name, last name, email and phone number.
    /// </summary>
    /// <param name="firstName">The first name.</param>
    /// <param name="lastName">The last name.</param>
    /// <param name="email">The email.</param>
    /// <param name="phoneNumber">The phone number.</param>
    /// <returns>The newly created contact instance.</returns>
    public static Contact Create(
        FirstName firstName,
        LastName lastName,
        Email email,
        PhoneNumber phoneNumber
    )
    {
        var contact = new Contact(firstName, lastName, email, phoneNumber);
        return contact;
    }
}
