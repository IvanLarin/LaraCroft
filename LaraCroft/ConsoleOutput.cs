namespace LaraCroft;

internal class ConsoleOutput : Output
{
    public void Write(string text) => Console.WriteLine(text);
}