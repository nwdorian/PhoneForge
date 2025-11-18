using FluentValidation;

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
        RuleFor(r => r.FirstName).NotEmpty().WithMessage("First name is required.");
        RuleFor(r => r.LastName).NotEmpty().WithMessage("Last name is required.");
        RuleFor(r => r.Email).NotEmpty().WithMessage("Email is required.");
        RuleFor(r => r.PhoneNumber).NotEmpty().WithMessage("Phone number is required.");
    }
}
