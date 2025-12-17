using Domain.Contacts;
using FluentValidation;

namespace WebApi.Contacts.Update;

/// <summary>
/// Validates the <see cref="UpdateContactRequest"/> instances.
/// </summary>
internal sealed class UpdateContactRequestValidator
    : AbstractValidator<UpdateContactRequest>
{
    /// <summary>
    /// Defines the validation rules for <see cref="UpdateContactRequest"/>
    /// </summary>
    public UpdateContactRequestValidator()
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
