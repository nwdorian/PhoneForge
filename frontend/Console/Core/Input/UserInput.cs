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
}
