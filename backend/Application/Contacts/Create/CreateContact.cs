using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PhoneForge.Domain.Contacts;
using PhoneForge.UseCases.Abstractions;
using PhoneForge.UseCases.Abstractions.Data;
using SharedKernel;

namespace PhoneForge.UseCases.Contacts.Create;

/// <summary>
/// Represents the <see cref="CreateContactRequest"/> handler.
/// </summary>
public sealed class CreateContact : IUseCase
{
    private readonly IDbContext _context;
    private readonly ILogger<CreateContact> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateContact"/> class.
    /// </summary>
    /// <param name="context">The database context used to persist the new contact.</param>
    /// <param name="logger">The logger used to record diagnostic information</param>
    public CreateContact(IDbContext context, ILogger<CreateContact> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// Handles a command.
    /// </summary>
    /// <returns>Response from the command.</returns>
    public async Task<Result<CreateContactResponse>> Handle(
        CreateContactRequest request,
        CancellationToken cancellationToken
    )
    {
        _logger.LogInformation("Processing request {Request}", request);

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
            _logger.LogWarning("Error: {@Error}", firstFailOrSuccess.Error);
            return firstFailOrSuccess.Error;
        }

        if (
            await _context.Contacts.AnyAsync(
                c => c.Email.Value == request.Email,
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
            contact.FirstName,
            contact.LastName,
            contact.FullName,
            contact.Email,
            contact.PhoneNumber,
            contact.CreatedOnUtc
        );

        _logger.LogInformation("Completed request {@Request}", request);
        return response;
    }
}
