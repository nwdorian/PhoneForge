namespace Console.Contacts;

internal sealed record Contact(
    Guid Id,
    string FirstName,
    string LastName,
    string FullName,
    string Email,
    string PhoneNumber,
    DateTime CreatedOnUtc
);
