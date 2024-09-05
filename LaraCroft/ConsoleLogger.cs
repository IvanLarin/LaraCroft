using Common;

namespace LaraCroft;

internal class ConsoleLogger : Logger
{
    private bool lineIsUnderUpdate;

    public void Log(string message) => WithResetUpdating(() => Console.WriteLine(Environment.NewLine + message));

    public void LogSuccess(string message) => WithColor(ConsoleColor.DarkGreen, () => Log(message));

    public void LogError(string message) => WithColor(ConsoleColor.DarkRed, () => Log(message));

    public void UpdateLine(string message)
    {
        Console.SetCursorPosition(0, Console.CursorTop - (lineIsUnderUpdate ? 1 : 0));
        Console.WriteLine(message);

        lineIsUnderUpdate = true;
    }

    private void WithResetUpdating(Action whatToDo)
    {
        lineIsUnderUpdate = false;
        whatToDo();
    }

    private void WithColor(ConsoleColor color, Action doIt) => AwesomeConsole.WithColor(color, doIt);
}