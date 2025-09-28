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
using LaraCroft.ValueObjects;

namespace LaraCroft;

internal class TheFactory : Factory
{
    private readonly Config config = new JsonConfig();

    private readonly HttpClient httpClient = new();

    private readonly Logger logger = new ConcurrentLogger(new ConsoleLogger());

    public PlaceToPut<Candle[]> MakeCandlePlace(Ticker ticker, Interval interval) =>
        new BadCandlesRemove(new CandlesTxtFile(ticker, interval, config));

    public Digger<Candle[]> MakeCandleDigger(Interval interval) =>
        new CandlesDigger(interval, this, this, MakeCandlesDownloader(interval));

    public BackDownloader MakeBackDownloader() =>
        new TheBackDownloader(httpClient, config, logger);

    public TheCandlesDownloader MakeCandlesDownloader(Interval interval) =>
        new TheCandlesDownloader(interval, MakeCandlesParser(), MakeBackDownloader());

    private Parser<Candle[]> MakeCandlesParser() => new CandlesJsonParser();

    public Excavator MakeExcavator(PlaceToPut<Candle[]> placeToPut, Ticker ticker, Interval interval, ProgressTracker<ShareProgress> tracker, CancellationToken token) =>
        new TheExcavator(placeToPut, ticker, MakeHistoryOf(ticker, interval, token), tracker);

    private History MakeHistoryOf(Ticker ticker, Interval interval, CancellationToken token) =>
        new MoexHistory(
            ticker, MakeSplitsDownloader(), MakeCandlesDownloader(interval), token);

    private SplitsDownloader MakeSplitsDownloader() =>
        new TheSplitsDownloader(MakeSplitsParser(), MakeBackDownloader());

    private Parser<Split[]> MakeSplitsParser() => new SplitsJsonParser();

    public PlaceToPut<(Spec, Border)> MakeSpecPlace(Ticker ticker) => new SpecFile(ticker, config);

    public Digger<(Spec, Border)> MakeSpecDigger(Interval interval) => new SpecDigger(MakeSpecDownloader(), MakeBorderDownloader(interval));

    private SpecDownloader MakeSpecDownloader() => new TheSpecDownloader(new SpecJsonParser(), MakeBackDownloader());

    private BorderDownloader MakeBorderDownloader(Interval interval) =>
        new TheBorderDownloader(new BorderJsonParser(interval), MakeBackDownloader());

    public Place<Candle[]> MakeInMemoryCandlePlace() => new CandlePlace();

    public VolumeCalculator MakeVolumeCalculator() => new TheVolumeCalculator();

    public SharesDownloader MakeSharesDownloader() =>
        new TheSharesDownloader(MakeSharesParser(), MakeBackDownloader());

    private Parser<Share[]> MakeSharesParser() => new SharesXmlParser();

    public ProgressTracker<ShareProgress> MakeProgressTracker(ShareProgress[] initialProgress) =>
        new SharesProgressTracker(initialProgress, this);

    public ProgressDisplay<ShareProgress> MakeProgressDisplay() => new ShareProgressDisplay(logger);

    public Lara MakeLara() => new TheLara(this, MakeInput(), logger);

    private Input MakeInput() => new TheInput(logger);
}