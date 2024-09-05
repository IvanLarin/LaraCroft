using Common;

namespace ConsoleFace;

internal class Success(Mind mind) : Part
{
    public void Do()
    {
        Console.WriteLine();
        AwesomeConsole.WriteLineWithColor(ConsoleColor.DarkGreen, "Миссия выполнена");

        mind.BecomeMainMenu();
    }
}