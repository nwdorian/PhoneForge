using FluentValidation;

namespace PhoneForge.WebApi.Endpoints.V1.Contacts.Create;

/// <summary>
/// Represents the <see cref="CreateContactRequest"/> class.
/// </summary>
public class CreateContactRequestValidator : AbstractValidator<CreateContactRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateContactRequestValidator"/> class.
    /// </summary>
    public CreateContactRequestValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("The first name is required.");
        RuleFor(x => x.LastName).NotEmpty().WithMessage("The last name is required.");
        RuleFor(x => x.Email).NotEmpty().WithMessage("The email is required.");
        RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("The phone number is required.");
    }
}
