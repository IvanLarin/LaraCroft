namespace LaraCroft;

internal interface Downloader
{
    Task<string> Download(string url);
}