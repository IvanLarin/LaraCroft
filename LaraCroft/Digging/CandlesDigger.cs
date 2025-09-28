using Common;
using LaraCroft.Downloading;
using LaraCroft.Entities;
using LaraCroft.ProgressTracking;
using LaraCroft.ValueObjects;

namespace LaraCroft.Digging;

internal class CandlesDigger(
    Interval interval,
    ExcavatorFactory excavatorFactory,
    TrackerFactory<ShareProgress> trackerFactory,
    CandlesDownloader candlesDownloader) : Digger<Candle[]>
{
    public async Task Dig(Work<Candle[]>[] works)
    {
        using var cts = new CancellationTokenSource();

        using ProgressTracker<ShareProgress> tracker =
            await MakeTracker(works.Select(w => w.Ticker).ToArray(), cts.Token);

        await works.ForEachAsync(cts.Token, body: async (work, token) =>
        {
            var excavator = excavatorFactory.MakeExcavator(work.PlaceToPut, work.Ticker, interval, tracker, token);

            await excavator.Dig();
        }, onException: _ => cts.Cancel());
    }

    private async Task<ProgressTracker<ShareProgress>> MakeTracker(Ticker[] tickers, CancellationToken token)
    {
        IEnumerable<Task<ShareProgress>> tasks = tickers.Select(async ticker =>
            GetInitialProgress(ticker, await candlesDownloader.Download(new CandlesDownloaderProps
            {
                FromPosition = 0,
                Ticker = ticker
            }, token)));

        ShareProgress[] initialProgress = await Task.WhenAll(tasks);

        ProgressTracker<ShareProgress> progressTracker = trackerFactory.MakeProgressTracker(initialProgress);

        return progressTracker;
    }

    private ShareProgress GetInitialProgress(Ticker ticker, Candle[] candles)
    {
        var candle = candles.MinBy(c => c.Begin);
        if (candle == null)
            return new ShareProgress { Date = DateTime.Today, IsCompleted = true, Ticker = ticker };

        return new ShareProgress { Date = candle.Begin, IsCompleted = false, Ticker = ticker };
    }
}