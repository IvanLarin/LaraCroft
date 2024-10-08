﻿using LaraCroft.Configuration;
using LaraCroft.Logging;

namespace LaraCroft.Downloading;

internal class TheDownloader(HttpClient client, Config config, Logger logger, CancellationToken token) : Downloader
{
    public Task<string> Download(string url) => DoDownload(WithHandlingException(WithRetries(FromThis(url)), url));

    private Task<string> DoDownload(Func<Task<string>> fn) => fn();

    private Func<Task<string>> FromThis(string url) => () => client.GetStringAsync(url);

    private Func<Task<string>> WithRetries(Func<Task<string>> fn) => async Task<string> () =>
    {
        var attempt = 0;
        while (true)
            try
            {
                token.ThrowIfCancellationRequested();

                return await fn();
            }
            catch (Exception e)
            {
                logger.WriteError(e);

                attempt++;
                if (attempt >= config.TryCountToDownload)
                    throw;

                await Task.Delay(config.DelayBetweenTriesInMilliseconds);
            }
    };

    private Func<Task<string>> WithHandlingException(Func<Task<string>> fn, string url) => async () =>
    {
        try
        {
            return await fn();
        }
        catch (Exception e)
        {
            throw new GoodException($"Ошибка GET запроса на сервер по такому запросу: \"{url}\"", e);
        }
    };
}