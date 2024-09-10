namespace LaraCroft;

internal class TheFactory : Factory
{
    private readonly Config config = new JsonConfig();

    private readonly HttpClient httpClient = new();

    private readonly Logger logger = new ConsoleLogger();

    public Input MakeInput() => new TheInput();

    public Excavator MakeExcavator(string ticker, int timeframeInMinutes, PlaceToPut placeToPut,
        CancellationToken token) =>
        new TheExcavator(MakeHistoryOf(ticker, timeframeInMinutes, token), placeToPut, MakeLogger());

    public Logger MakeLogger() => logger;

    public PlaceToPut MakeFile(string ticker, int timeframeInMinutes) =>
        new TxtFile(ticker, timeframeInMinutes, config);

    public SharesGetter MakeSharesGetter(CancellationToken token = default) =>
        new TheSharesGetter(MakeDownloader(token), MakeSharesParser());

    public CandleBuffer MakeCandleBuffer() => new TheCandleBuffer();

    public ShareStatistics MakeShareStatistics() => new TheShareStatistics();

    public Output MakeOutput() => new ConsoleOutput();

    public Lara MakeLara() => new TheLara(this);

    public History MakeHistoryOf(string ticker, int timeframeInMinutes, CancellationToken token = default) => new MoexHistory(
        ticker, timeframeInMinutes,
        MakeDownloader(token), MakeCandlesParser(), MakeSplitsParser());

    private Downloader MakeDownloader(CancellationToken token) =>
        new TheDownloader(httpClient, config, MakeLogger(), token);

    private Parser<Split[]> MakeSplitsParser() => new XmlSplitsParser();

    private Parser<Candle[]> MakeCandlesParser() => new XmlCandlesParser();

    private Parser<Share[]> MakeSharesParser() => new XmlSharesParser();
}