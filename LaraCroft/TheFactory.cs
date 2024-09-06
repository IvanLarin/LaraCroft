namespace LaraCroft;

internal class TheFactory : Factory
{
    private readonly Config config = new JsonConfig();

    private readonly HttpClient httpClient = new();

    private readonly Logger logger = new ConsoleLogger();

    public Input MakeInput() => new TheInput();

    public Excavator MakeExcavator(string ticker, int timeframeInMinutes, PlaceToPut placeToPut) =>
        new TheExcavator(MakeHistoryOf(ticker, timeframeInMinutes), placeToPut, MakeLogger());

    public Logger MakeLogger() => logger;

    public PlaceToPut MakeFilePlaceToPut(string ticker, int timeframeInMinutes) =>
        new TxtFile(ticker, timeframeInMinutes, config);

    public SharesGetter MakeSharesGetter() => new TheSharesGetter(MakeDownloader(), MakeSharesParser());

    public CandleBuffer MakeCandleBuffer() => new TheCandleBuffer();

    public ShareStatistics MakeShareStatistics() => new TheShareStatistics();

    public Output MakeOutput() => new ConsoleOutput();

    public Lara MakeLara() => new TheLara(this);

    private History MakeHistoryOf(string ticker, int timeframeInMinutes) => new MoexHistory(ticker, timeframeInMinutes,
        MakeDownloader(), MakeCandlesParser(), MakeSplitsParser());

    private Downloader MakeDownloader() => new TheDownloader(httpClient, config, MakeLogger());

    private Parser<Split[]> MakeSplitsParser() => new XmlSplitsParser();

    private Parser<Candle[]> MakeCandlesParser() => new XmlCandlesParser();

    private Parser<Share[]> MakeSharesParser() => new XmlSharesParser();
}