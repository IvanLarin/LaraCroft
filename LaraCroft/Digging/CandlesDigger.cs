using Common;
using LaraCroft.Downloading;
using LaraCroft.Entities;
using LaraCroft.ProgressTracking;

namespace LaraCroft.Digging;

internal class CandlesDigger(
    ExcavatorFactory excavatorFactory,
    TrackerFactory<ShareProgress> trackerFactory,
    CandlesDownloaderFactory candlesDownloaderFactory,
    int interval) : Digger<Candle[]>
{
    public async Task Dig(Work<Candle[]>[] works)
    {
        using ProgressTracker<ShareProgress> tracker =
            await MakeTracker(works.Select(w => w.Ticker).ToArray());

        using var cts = new CancellationTokenSource();

        await works.ForEachAsync(cts.Token, body: async (work, token) =>
        {
            var excavator = excavatorFactory.MakeExcavator(work.PlaceToPut, work.Ticker,
                interval, tracker, token);

            await excavator.Dig();
        }, onException: _ => cts.Cancel());
    }

    private async Task<ProgressTracker<ShareProgress>> MakeTracker(string[] tickers)
    {
        var downloader = candlesDownloaderFactory.MakeCandlesDownloader(interval: 1);

        IEnumerable<Task<ShareProgress>> tasks = tickers.Select(async ticker =>
            GetInitialProgress(ticker, await downloader.Download(ticker, 0)));

        ShareProgress[] initialProgress = await Task.WhenAll(tasks);

        ProgressTracker<ShareProgress> progressTracker = trackerFactory.MakeProgressTracker(initialProgress);

        return progressTracker;
    }

    private ShareProgress GetInitialProgress(string ticker, Candle[] candles)
    {
        var candle = candles.MinBy(c => c.Begin);
        if (candle == null)
            return new ShareProgress { Date = DateTime.Today, IsCompleted = true, Ticker = ticker };

        return new ShareProgress { Date = candle.Begin, IsCompleted = false, Ticker = ticker };
    }
}