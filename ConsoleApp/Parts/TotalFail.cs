using Common;

namespace ConsoleApp.Parts;

internal class TotalFail(Mind mind) : Part
{
    public void Do()
    {
        Console.WriteLine();
        AwesomeConsole.WriteLineWithColor(ConsoleColor.DarkRed, "Ужасная ошибка. Катастрофа. конец... отвалился");

        Console.ReadLine();

        mind.BecomeGoodbye();
    }
}