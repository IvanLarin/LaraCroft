namespace LaraCroft;

public interface Logger
{
    void Log(string message);

    void LogSuccess(string message);

    void LogError(string message);

    void UpdateLine(string message);
}