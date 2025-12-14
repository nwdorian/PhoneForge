using Spectre.Console;

namespace Console.Contacts.Get;

internal static class SortOrderMenu
{
    public static string Display()
    {
        SortOrderOptions selection = AnsiConsole.Prompt(
            new SelectionPrompt<SortOrderOptions>()
                .Title("Select sorting order:")
                .AddChoices(Enum.GetValues<SortOrderOptions>())
        );

        return selection switch
        {
            SortOrderOptions.Ascending => "asc",
            SortOrderOptions.Descending => "desc",
            _ => "Unknown sort order option",
        };
    }

    private enum SortOrderOptions
    {
        Ascending,
        Descending,
    }
}
