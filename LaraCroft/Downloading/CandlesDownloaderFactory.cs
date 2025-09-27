namespace LaraCroft.Downloading;

internal interface CandlesDownloaderFactory
{
    CandlesDownloader MakeCandlesDownloader(int interval, CancellationToken token = default);
}