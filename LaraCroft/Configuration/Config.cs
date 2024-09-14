namespace LaraCroft.Configuration;

internal interface Config
{
    string OutputDirectory { get; }

    int TryCountToDownload { get; }

    int DelayBetweenTriesInMilliseconds { get; }
}