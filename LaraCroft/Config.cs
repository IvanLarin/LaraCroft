namespace LaraCroft;

internal interface Config
{
    string OutputDirectory { get; }

    int TryCountToDownload { get; }

    int DelayBetweenTriesInMilliseconds { get; }
}