namespace LaraCroft.Downloading;

internal interface Downloader<in TProps, TResult>
{
    Task<TResult> Download(TProps props, CancellationToken token = default);
}