using Common;

namespace ConsoleApp.Parts;

internal class Success(Mind mind) : Part
{
    public void Do()
    {
        Console.WriteLine();
        AwesomeConsole.WriteLineWithColor(ConsoleColor.DarkGreen, "Готово!");

        mind.BecomeMainMenu();
    }
}