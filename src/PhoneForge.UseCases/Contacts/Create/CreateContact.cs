using Microsoft.EntityFrameworkCore;
using PhoneForge.Domain.Contacts;
using PhoneForge.UseCases.Abstractions.Data;
using SharedKernel;

namespace PhoneForge.UseCases.Contacts.Create;

/// <summary>
/// Represents the <see cref="CreateContactCommand"/> handler.
/// </summary>
public sealed class CreateContact
{
    private readonly IDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateContact"/> class.
    /// </summary>
    /// <param name="context">The database context used to persist the new contact.</param>
    public CreateContact(IDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Handles a request.
    /// </summary>
    /// <returns>Response from the request.</returns>
    public async Task<Result<CreateContactResponse>> Handle(
        CreateContactCommand request,
        CancellationToken cancellationToken
    )
    {
        var firstNameResult = FirstName.Create(request.FirstName);
        var lastNameResult = LastName.Create(request.LastName);
        var emailResult = Email.Create(request.Email);
        var phoneNumberResult = PhoneNumber.Create(request.PhoneNumber);

        var firstFailOrSuccess = Result.FirstFailOrSuccess(
            firstNameResult,
            lastNameResult,
            emailResult,
            phoneNumberResult
        );

        if (firstFailOrSuccess.IsFailure)
        {
            return firstFailOrSuccess.Error;
        }

        if (
            await _context.Contacts.AnyAsync(
                c => c.Email.Value == request.Email,
                cancellationToken
            )
        )
        {
            return ContactErrors.EmailNotUnique;
        }

        var contact = Contact.Create(
            firstNameResult.Value,
            lastNameResult.Value,
            emailResult.Value,
            phoneNumberResult.Value
        );

        _context.Contacts.Add(contact);

        await _context.SaveChangesAsync(cancellationToken);

        var response = new CreateContactResponse(
            contact.Id,
            contact.FirstName,
            contact.LastName,
            contact.FullName,
            contact.Email,
            contact.PhoneNumber,
            contact.CreatedOnUtc
        );

        return response;
    }
}
