namespace LaraCroft.Downloading;

internal interface Downloader
{
    Task<string> Download(string url);
}