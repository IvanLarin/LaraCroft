namespace LaraCroft;

internal interface Config
{
    string OutputDirectory { get; }

    int CandleDurationInMinutes { get; }
}