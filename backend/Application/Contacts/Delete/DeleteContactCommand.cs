namespace Application.Contacts.Delete;

/// <summary>
/// Represents the command for deleting a contact.
/// </summary>
/// <param name="Id">The unique identifier of the contact to delete.</param>
public sealed record class DeleteContactCommand(Guid Id);
