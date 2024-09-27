using Common;

namespace LaraCroft.Logging;

internal class ConsoleLogger : Logger
{
    public void WriteLine(string value = "") => AvoidingBufferOverflow(Console.WriteLine)(value);

    public void WriteErrorLine(string value) =>
        AvoidingBufferOverflow(v => AwesomeConsole.WriteLineWithColor(ConsoleColor.DarkRed, v))(value);

    private Action<string> AvoidingBufferOverflow(Action<string> fn) => value =>
    {
        var linesInValue = value.Split(Environment.NewLine).Length - 1;
        var linesWillBeWritten = linesInValue + 1;

        if (Console.GetCursorPosition().Top + linesWillBeWritten > Console.BufferHeight - 1)
            Console.Clear();

        fn(value);
    };

    public Logger HoldThisPosition() => new HoldPositionLogger(this);

    public (int Left, int Top) GetCursorPosition() => Console.GetCursorPosition();

    public bool CursorVisible
    {
        get => OperatingSystem.IsWindows() && Console.CursorVisible;
        set
        {
            if (OperatingSystem.IsWindows())
                Console.CursorVisible = value;
        }
    }

    public void SetCursorPosition(int left, int top) => Console.SetCursorPosition(left, top);

    public void WriteError(Exception exception) => AwesomeConsole.WriteError(exception);
}