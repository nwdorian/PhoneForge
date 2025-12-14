using Console.Contacts.Get;
using Console.Core.Input;
using Spectre.Console;

namespace Console.Contacts;

internal sealed class ContactsMenu(GetContacts getContacts)
{
    public async Task Display()
    {
        bool exit = false;

        while (!exit)
        {
            AnsiConsole.Clear();

            AnsiConsole.Write(
                new FigletText("Phone Forge").LeftJustified().Color(Color.Yellow)
            );

            MenuOptions selection = await AnsiConsole.PromptAsync(
                new SelectionPrompt<MenuOptions>()
                    .Title("Select from the menu:")
                    .UseConverter(PrintMenuOptions)
                    .AddChoices(Enum.GetValues<MenuOptions>())
            );

            switch (selection)
            {
                case MenuOptions.GetAll:
                    await getContacts.Handle();
                    break;
                case MenuOptions.Add:
                    break;
                case MenuOptions.Delete:
                    break;
                case MenuOptions.Update:
                    break;
                case MenuOptions.Exit:
                    exit = UserInput.ConfirmExit();
                    break;
            }
        }
    }

    private static string PrintMenuOptions(MenuOptions options)
    {
        return options switch
        {
            MenuOptions.GetAll => "Get all contacts",
            MenuOptions.Add => "Add contact",
            MenuOptions.Delete => "Delete contact",
            MenuOptions.Update => "Update contact",
            MenuOptions.Exit => "Exit",
            _ => "Unknown contacts menu option",
        };
    }

    private enum MenuOptions
    {
        GetAll,
        Add,
        Delete,
        Update,
        Exit,
    }
}
