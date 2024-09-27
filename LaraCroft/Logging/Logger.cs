namespace LaraCroft.Logging;

public interface Logger
{
    void WriteLine(string value = "");

    void WriteErrorLine(string value = "");

    Logger HoldThisPosition();

    (int Left, int Top) GetCursorPosition();

    bool CursorVisible { get; set; }

    void SetCursorPosition(int left, int top);

    void WriteError(Exception exception);
}