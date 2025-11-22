using Application.Core.Abstractions;
using Application.Core.Abstractions.Data;
using Domain.Contacts;
using Domain.Core.Primitives;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Contacts.Create;

/// <summary>
/// Represents the <see cref="CreateContact"/> use case.
/// </summary>
public sealed class CreateContact : IUseCase
{
    private readonly IDbContext _context;
    private readonly ILogger<CreateContact> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateContact"/> class.
    /// </summary>
    /// <param name="context">The database context used to persist the new contact.</param>
    /// <param name="logger">The logger used to record diagnostic information.</param>
    public CreateContact(IDbContext context, ILogger<CreateContact> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// Represents the <see cref="CreateContactCommand"/> handler.
    /// </summary>
    /// <param name="command">The command containing the new contact information.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the operation.</param>
    /// <returns>
    /// A successful result containing a <see cref="ContactResponse"/> if the contact was created
    /// or an error result.
    /// </returns>
    public async Task<Result<CreateContactResponse>> Handle(
        CreateContactCommand command,
        CancellationToken cancellationToken
    )
    {
        _logger.LogInformation("Processing command {Command}", command);

        var firstNameResult = FirstName.Create(command.FirstName);
        var lastNameResult = LastName.Create(command.LastName);
        var emailResult = Email.Create(command.Email);
        var phoneNumberResult = PhoneNumber.Create(command.PhoneNumber);

        var firstFailOrSuccess = Result.FirstFailOrSuccess(
            firstNameResult,
            lastNameResult,
            emailResult,
            phoneNumberResult
        );

        if (firstFailOrSuccess.IsFailure)
        {
            _logger.LogWarning("Error: {@Error}", firstFailOrSuccess.Error);
            return firstFailOrSuccess.Error;
        }

        if (
            await _context.Contacts.AnyAsync(
                c => c.Email.Value == command.Email,
                cancellationToken
            )
        )
        {
            _logger.LogWarning("Error: {@Error}", ContactErrors.EmailNotUnique);
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
            contact.FullName,
            contact.Email,
            contact.PhoneNumber,
            contact.CreatedOnUtc
        );

        _logger.LogInformation("Completed command {@Command}", command);
        return response;
    }
}
