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

    public Excavator MakeExcavator(PlaceToPut<Candle[]> placeToPut, string ticker, int interval,
        ProgressTracker<ShareProgress> tracker, CancellationToken token = default) =>
        new TheExcavator(placeToPut, ticker, MakeHistoryOf(ticker, interval, token), tracker);

    public PlaceToPut<Candle[]> MakeCandlePlace(string ticker, int interval) =>
        new BadCandlesRemove(new TxtFile(ticker, interval, config));

    public SharesDownloader MakeSharesDownloader(CancellationToken token = default) =>
        new TheSharesDownloader(MakeDownloader(token), MakeSharesParser());

    public Downloader MakeDownloader(CancellationToken token) =>
        new TheDownloader(httpClient, config, logger, token);

    public Place<Candle[]> MakeInMemoryCandlePlace() => new CandlePlace();

    public VolumeCalculator MakeVolumeCalculator() => new TheVolumeCalculator();

    public Digger<Candle[]> MakeCandleDigger(int interval) => new CandlesDigger(this, this, this, interval);

    public PlaceToPut<(Spec, CandlesBorder)> MakeSpecPlace(string ticker) => new SpecFile(ticker, config);

    public Digger<(Spec, CandlesBorder)> MakeSpecDigger(int interval) => new SpecDigger(interval, this, new SpecJsonParser(), new CandlesBordersJsonParser());

    public ProgressTracker<ShareProgress> MakeProgressTracker(ShareProgress[] initialProgress) =>
        new SharesProgressTracker(initialProgress, this);

    public CandlesDownloader MakeCandlesDownloader(int interval, CancellationToken token) =>
        new TheCandlesDownloader(interval, MakeDownloader(token), MakeCandlesParser());

    public ProgressDisplay<ShareProgress> MakeProgressDisplay() => new ShareProgressDisplay(logger);

    public Lara MakeLara() => new TheLara(this, MakeInput(), logger);

    private Input MakeInput() => new TheInput(logger);

    private History MakeHistoryOf(string ticker, int interval, CancellationToken token = default) =>
        new MoexHistory(
            ticker, MakeDownloader(token), MakeSplitsParser(), MakeCandlesDownloader(interval, token));

    private Parser<Split[]> MakeSplitsParser() => new SplitsJsonParser();

    private Parser<Candle[]> MakeCandlesParser() => new CandlesJsonParser();

    private Parser<Share[]> MakeSharesParser() => new SharesXmlParser();
}