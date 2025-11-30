using Application.Core.Abstractions.Data;
using Application.Core.Abstractions.Messaging;
using Domain.Contacts;
using Domain.Core.Primitives;
using Microsoft.EntityFrameworkCore;

namespace Application.Contacts.Create;

internal sealed class CreateContact(IDbContext context)
    : ICommandHandler<CreateContactCommand, ContactResponse>
{
    public async Task<Result<ContactResponse>> Handle(
        CreateContactCommand command,
        CancellationToken cancellationToken
    )
    {
        Result<FirstName> firstNameResult = FirstName.Create(command.FirstName);
        Result<LastName> lastNameResult = LastName.Create(command.LastName);
        Result<Email> emailResult = Email.Create(command.Email);
        Result<PhoneNumber> phoneNumberResult = PhoneNumber.Create(command.PhoneNumber);

        Result firstFailOrSuccess = Result.FirstFailOrSuccess(
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
            await context.Contacts.AnyAsync(
                c => c.Email == emailResult.Value,
                cancellationToken
            )
        )
        {
            return ContactErrors.EmailNotUnique;
        }

        Contact contact = Contact.Create(
            firstNameResult.Value,
            lastNameResult.Value,
            emailResult.Value,
            phoneNumberResult.Value
        );

        context.Contacts.Add(contact);

        await context.SaveChangesAsync(cancellationToken);

        ContactResponse response = new(
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
