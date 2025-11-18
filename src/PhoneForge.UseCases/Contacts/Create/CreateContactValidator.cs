using FluentValidation;

namespace PhoneForge.UseCases.Contacts.Create;

public sealed class CreateContactValidator : AbstractValidator<CreateContactRequest>
{
    public CreateContactValidator()
    {
        RuleFor(r => r.FirstName).NotEmpty().WithMessage("First name is required.");
        RuleFor(r => r.LastName).NotEmpty().WithMessage("Last name is required.");
        RuleFor(r => r.Email).NotEmpty().WithMessage("Email is required.");
        RuleFor(r => r.PhoneNumber).NotEmpty().WithMessage("Phone number is required.");
    }
}
