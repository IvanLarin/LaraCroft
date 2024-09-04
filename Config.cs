namespace LaraCroft;

public interface Config
{
    string OutputDirectory { get; }

    int CandleDurationInMinutes { get; }
}