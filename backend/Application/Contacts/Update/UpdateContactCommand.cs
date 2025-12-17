using Application.Core.Abstractions.Messaging;

namespace Application.Contacts.Update;

/// <summary>
/// Represents the command for deleting a contact.
/// </summary>
/// <param name="Id">The identifier of the contact.</param>
/// <param name="FirstName">The first name of the contact.</param>
/// <param name="LastName">The last name of the contact.</param>
/// <param name="Email">The email of the contact.</param>
/// <param name="PhoneNumber">The phone number of the contact.</param>
public record class UpdateContactCommand(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber
) : ICommand;
