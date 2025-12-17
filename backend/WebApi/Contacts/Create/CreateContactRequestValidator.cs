using Domain.Contacts;
using FluentValidation;

namespace WebApi.Contacts.Create;

/// <summary>
/// Validates <see cref="CreateContactRequest"/> instances.
/// </summary>
internal sealed class CreateContactRequestValidator
    : AbstractValidator<CreateContactRequest>
{
    /// <summary>
    /// Defines the validation rules for a <see cref="CreateContactRequest"/>.
    /// </summary>
    public CreateContactRequestValidator()
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
