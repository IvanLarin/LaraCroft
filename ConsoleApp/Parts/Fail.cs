using Common;

namespace ConsoleApp.Parts;

internal class Fail(Mind mind) : Part
{
    public void Do()
    {
        Console.WriteLine();
        AwesomeConsole.WriteLineWithColor(ConsoleColor.DarkRed, "Не получилось");

        mind.BecomeMainMenu();
    }
}