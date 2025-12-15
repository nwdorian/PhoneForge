using Spectre.Console;

namespace Console.Core.Validation;

internal static class InputValidation
{
    public static ValidationResult IsGreaterThanOrEqualToZero(int input)
    {
        return input switch
        {
            < 0 => ValidationResult.Error(
                "[red]Input must be greater than or equal to zero![/]"
            ),
            _ => ValidationResult.Success(),
        };
    }

    public static ValidationResult IsGreaterThanZero(int input)
    {
        return input switch
        {
            < 1 => ValidationResult.Error("[red]Input must be greater than 0![/]"),
            _ => ValidationResult.Success(),
        };
    }

    public static ValidationResult IsValidEmail(string input)
    {
        if (!input.Contains('@'))
        {
            return ValidationResult.Error("[red]Invalid email format![/]");
        }

        return ValidationResult.Success();
    }

    public static ValidationResult IsValidPhoneNumber(string input)
    {
        if (!input.All(char.IsDigit))
        {
            return ValidationResult.Error(
                "[red]Invalid phone number! Only numbers are allowed.[/]"
            );
        }

        if (input.Length < 6)
        {
            return ValidationResult.Error("[red]Minimum phone number length is 6![/]");
        }

        if (input.Length > 20)
        {
            return ValidationResult.Error("[red]Maximum phone number length is 20![/]");
        }

        return ValidationResult.Success();
    }
}
