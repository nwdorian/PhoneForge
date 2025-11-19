using FluentValidation;
using PhoneForge.Domain.Contacts;

namespace PhoneForge.UseCases.Contacts.Create;

/// <summary>
/// Validates <see cref="CreateContactRequest"/> instances.
/// </summary>
public sealed class CreateContactValidator : AbstractValidator<CreateContactRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateContactValidator"/> class
    /// and defines the validation rules for a <see cref="CreateContactRequest"/>.
    /// </summary>
    public CreateContactValidator()
    {
        RuleFor(r => r.FirstName)
            .NotEmpty()
            .WithMessage(ContactErrors.FirstName.IsRequired.Description);

        RuleFor(r => r.LastName)
            .NotEmpty()
            .WithMessage(ContactErrors.LastName.IsRequired.Description);

        RuleFor(r => r.Email)
            .NotEmpty()
            .WithMessage(ContactErrors.Email.IsRequired.Description);

        RuleFor(r => r.PhoneNumber)
            .NotEmpty()
            .WithMessage(ContactErrors.PhoneNumber.IsRequired.Description);
    }
}
