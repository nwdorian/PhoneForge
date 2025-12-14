using Spectre.Console;

namespace Console.Contacts.Get;

internal static class SortColumnMenu
{
    public static string Display()
    {
        SortColumnOptions selection = AnsiConsole.Prompt(
            new SelectionPrompt<SortColumnOptions>()
                .Title("Select sorting column:")
                .UseConverter(PrintSortColumnOptions)
                .AddChoices(Enum.GetValues<SortColumnOptions>())
        );

        return selection switch
        {
            SortColumnOptions.FirstName => "first_name",
            SortColumnOptions.LastName => "last_name",
            SortColumnOptions.Email => "email",
            SortColumnOptions.PhoneNumber => "phone_number",
            SortColumnOptions.CreatedOn => "created_on",
            _ => "Unkown sort column option.",
        };
    }

    private static string PrintSortColumnOptions(SortColumnOptions options)
    {
        return options switch
        {
            SortColumnOptions.FirstName => "First name",
            SortColumnOptions.LastName => "Last name",
            SortColumnOptions.Email => "Email",
            SortColumnOptions.PhoneNumber => "Phone number",
            SortColumnOptions.CreatedOn => "Date created",
            _ => "Unkown sort column option.",
        };
    }

    private enum SortColumnOptions
    {
        FirstName,
        LastName,
        Email,
        PhoneNumber,
        CreatedOn,
    }
}
