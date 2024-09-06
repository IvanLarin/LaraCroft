using Common;

namespace LaraCroft;

internal class TheDownloader(HttpClient client, Config config, Logger logger) : Downloader
{
    public Task<string> Download(string uri) =>
        StringDownloader(uri).Pipe(Retryer).Pipe(fn => HandleException(fn, uri));

    private Func<Task<string>> StringDownloader(string uri) => () => client.GetStringAsync(uri);

    private Func<Task<string>> Retryer(Func<Task<string>> fn) => async Task<string> () =>
    {
        var attempt = 0;
        while (true)
            try
            {
                return await fn();
            }
            catch (Exception e)
            {
                attempt++;
                if (attempt >= config.TryCountToDownload)
                    throw;

                logger.LogError(e.Message);
                await Task.Delay(config.DelayBetweenTriesInMilliseconds);
            }
    };


    private async Task<string> HandleException(Func<Task<string>> fn, string uri)
    {
        try
        {
            return await fn();
        }
        catch (HttpRequestException e)
        {
            throw new GoodException($$"""
                                      Ошибка GET запроса на сервер по такому запросу: "{{uri}}"
                                      {{e.Message}}
                                      """, e);
        }
    }
}