namespace LaraCroft.ProgressTracking;

internal class SharesProgressTracker : ProgressTracker<ShareProgress>
{
    private readonly ProgressDisplay<ShareProgress> display;
    private readonly Dictionary<string, ShareProgress> tickers;

    public SharesProgressTracker(ShareProgress[] initialProgress, ShareProgressDisplayFactory factory)
    {
        display = factory.MakeProgressDisplay();

        tickers = new Dictionary<string, ShareProgress>(initialProgress.Select(share =>
            new KeyValuePair<string, ShareProgress>(share.Ticker, share)));
    }

    private readonly object lockObject = new();

    public void Report(ShareProgress shareProgress)
    {
        lock (lockObject)
        {
            UpdateTickers(shareProgress);
            display.Display(tickers.Values.ToArray());
        }
    }

    private void UpdateTickers(ShareProgress shareProgress) => tickers[shareProgress.Ticker] = shareProgress;

    public void Dispose() => display.Dispose();
}