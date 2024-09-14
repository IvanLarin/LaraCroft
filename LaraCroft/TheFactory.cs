using LaraCroft.Calculating;
using LaraCroft.Chronology;
using LaraCroft.Configuration;
using LaraCroft.Digging;
using LaraCroft.Downloading;
using LaraCroft.Entities;
using LaraCroft.Inputting;
using LaraCroft.Logging;
using LaraCroft.Parsing;
using LaraCroft.Placing;
using LaraCroft.ProgressTracking;

namespace LaraCroft;

internal class TheFactory : Factory
{
    private readonly Config config = new JsonConfig();

    private readonly HttpClient httpClient = new();

    private readonly Logger logger = new ConcurrentLogger(new ConsoleLogger());

    public Excavator MakeExcavator(PlaceToPut<Candle> placeToPut, string ticker, int timeframeInMinutes,
        ProgressTracker<ShareProgress> tracker, CancellationToken token = default) =>
        new TheExcavator(placeToPut, ticker, MakeHistoryOf(ticker, timeframeInMinutes, token), tracker);

    public PlaceToPut<Candle> MakeFile(string ticker, int timeframeInMinutes) =>
        new TxtFile(ticker, timeframeInMinutes, config);

    public SharesDownloader MakeSharesDownloader(CancellationToken token = default) =>
        new TheSharesDownloader(MakeDownloader(token), MakeSharesParser());

    private Downloader MakeDownloader(CancellationToken token) =>
        new TheDownloader(httpClient, config, logger, token);

    public Place<Candle> MakeCandlePlace() => new CandlePlace();

    public VolumeCalculator MakeVolumeCalculator() => new TheVolumeCalculator();

    public Digger MakeDigger() => new TheDigger(this, this, this, logger);

    public ProgressTracker<ShareProgress> MakeProgressTracker(ShareProgress[] initialProgress) =>
        new SharesProgressTracker(initialProgress, this);

    public CandlesDownloader MakeCandlesDownloader(int timeframeInMinutes, CancellationToken token) =>
        new TheCandlesDownloader(timeframeInMinutes, MakeDownloader(token), MakeCandlesParser());

    public ProgressDisplay<ShareProgress> MakeProgressDisplay() => new ShareProgressDisplay(logger);

    public Lara MakeLara() => new TheLara(this, MakeInput(), logger);

    private Input MakeInput() => new TheInput(logger);

    private History MakeHistoryOf(string ticker, int timeframeInMinutes, CancellationToken token = default) =>
        new MoexHistory(
            ticker, MakeDownloader(token), MakeSplitsParser(), MakeCandlesDownloader(timeframeInMinutes, token));

    private Parser<Split[]> MakeSplitsParser() => new XmlSplitsParser();

    private Parser<Candle[]> MakeCandlesParser() => new XmlCandlesParser();

    private Parser<Share[]> MakeSharesParser() => new XmlSharesParser();
}