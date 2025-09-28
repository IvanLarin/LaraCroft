using LaraCroft.Parsing;
using LaraCroft.ValueObjects;

namespace LaraCroft.Downloading;

internal abstract class BaseDownloader<TProps, TResult>(Parser<TResult> parser, Downloader<Url, TextFromBack> downloader) : Downloader<TProps, TResult>
{
    public async Task<TResult> Download(TProps props, CancellationToken token)
    {
        var url = GetUrl(props);

        var textFromBack = await downloader.Download(url, token);

        try
        {
            return parser.Parse(textFromBack);
        }
        catch (Exception ex)
        {
            throw new BackParseException(url, textFromBack, ex);
        }
    }

    protected abstract Url GetUrl(TProps props);
}