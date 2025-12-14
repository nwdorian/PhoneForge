using Console.Core.Enums;
using Spectre.Console;

namespace Console.Core.Input;

internal static class UserInput
{
    public static void PromptAnyKeyToContinue()
    {
        AnsiConsole.Write("Press any key to continue...");
        System.Console.ReadKey();
    }

    public static bool ConfirmExit()
    {
        if (AnsiConsole.Confirm("Are you sure you want to exit?"))
        {
            AnsiConsole.WriteLine("Goodbye!");
            return true;
        }
        return false;
    }

    public static string PromptString(string displayMessage, bool allowEmpty)
    {
        TextPrompt<string> prompt = new(displayMessage);

        if (allowEmpty)
        {
            prompt.AllowEmpty();
        }

        return AnsiConsole.Prompt(prompt);
    }

    public static string PromptSortOrder()
    {
        SortOrder selection = AnsiConsole.Prompt(
            new SelectionPrompt<SortOrder>()
                .Title("Select sorting order:")
                .AddChoices(Enum.GetValues<SortOrder>())
        );

        return selection switch
        {
            SortOrder.Ascending => "asc",
            SortOrder.Descending => "desc",
            _ => "Unknown sort order option",
        };
    }

    public static string PromptContactsSortColumn()
    {
        ContactsSortColumn selection = AnsiConsole.Prompt(
            new SelectionPrompt<ContactsSortColumn>()
                .Title("Select sorting column:")
                .UseConverter(ConvertSortColumnOptions)
                .AddChoices(Enum.GetValues<ContactsSortColumn>())
        );

        return selection switch
        {
            ContactsSortColumn.FirstName => "first_name",
            ContactsSortColumn.LastName => "last_name",
            ContactsSortColumn.Email => "email",
            ContactsSortColumn.PhoneNumber => "phone_number",
            ContactsSortColumn.CreatedOn => "created_on",
            _ => "Unkown sort column option.",
        };

        static string ConvertSortColumnOptions(ContactsSortColumn options)
        {
            return options switch
            {
                ContactsSortColumn.FirstName => "First name",
                ContactsSortColumn.LastName => "Last name",
                ContactsSortColumn.Email => "Email",
                ContactsSortColumn.PhoneNumber => "Phone number",
                ContactsSortColumn.CreatedOn => "Date created",
                _ => "Unkown sort column option.",
            };
        }
    }

    public static int PromptPageSize()
    {
        PageSize selection = AnsiConsole.Prompt(
            new SelectionPrompt<PageSize>()
                .Title("Select page size:")
                .UseConverter(ConvertPageSizeOptions)
                .AddChoices(Enum.GetValues<PageSize>())
        );

        return selection switch
        {
            PageSize.Ten => 10,
            PageSize.Twenty => 20,
            PageSize.Thirty => 30,
            _ => 10,
        };
        static string ConvertPageSizeOptions(PageSize options)
        {
            return options switch
            {
                PageSize.Ten => "10",
                PageSize.Twenty => "20",
                PageSize.Thirty => "30",
                _ => "Unkown page size option.",
            };
        }
    }
}
