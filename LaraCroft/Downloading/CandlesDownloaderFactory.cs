namespace LaraCroft.Downloading;

internal interface CandlesDownloaderFactory
{
    CandlesDownloader MakeCandlesDownloader(int timeframeInMinutes, CancellationToken token = default);
}