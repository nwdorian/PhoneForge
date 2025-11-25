using Application.Core.Abstractions.Data;
using Application.Core.Abstractions.Messaging;
using Domain.Contacts;
using Domain.Core.Primitives;
using Microsoft.EntityFrameworkCore;

namespace Application.Contacts.Update;

internal class UpdateContact(IDbContext context) : ICommandHandler<UpdateContactCommand>
{
    public async Task<Result> Handle(
        UpdateContactCommand command,
        CancellationToken cancellationToken
    )
    {
        Contact? contact = await context.Contacts.SingleOrDefaultAsync(
            c => c.Id == command.Id,
            cancellationToken
        );

        if (contact is null)
        {
            return ContactErrors.NotFoundById(command.Id);
        }

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

        contact.UpdateContact(
            firstNameResult.Value,
            lastNameResult.Value,
            emailResult.Value,
            phoneNumberResult.Value
        );

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
