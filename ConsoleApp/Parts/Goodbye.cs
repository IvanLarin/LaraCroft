namespace ConsoleApp.Parts;

internal class Goodbye(Mind mind) : Part
{
    public void Do()
    {
        Console.WriteLine();
        Console.WriteLine("Пока!");

        mind.BecomeNirvana();
    }
}