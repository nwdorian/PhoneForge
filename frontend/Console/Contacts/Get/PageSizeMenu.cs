using Spectre.Console;

namespace Console.Contacts.Get;

internal static class PageSizeMenu
{
    public static int Display()
    {
        PageSizeOptions selection = AnsiConsole.Prompt(
            new SelectionPrompt<PageSizeOptions>()
                .Title("Select page size:")
                .UseConverter(PrintPageSizeOptions)
                .AddChoices(Enum.GetValues<PageSizeOptions>())
        );

        return selection switch
        {
            PageSizeOptions.Ten => 10,
            PageSizeOptions.Twenty => 20,
            PageSizeOptions.Thirty => 30,
            _ => 10,
        };
    }

    private static string PrintPageSizeOptions(PageSizeOptions options)
    {
        return options switch
        {
            PageSizeOptions.Ten => "10",
            PageSizeOptions.Twenty => "20",
            PageSizeOptions.Thirty => "30",
            _ => "Unkown page size option.",
        };
    }

    private enum PageSizeOptions
    {
        Ten,
        Twenty,
        Thirty,
    }
}
