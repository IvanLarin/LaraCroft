namespace LaraCroft;

internal class TheFactory : Factory
{
    private Constants? constants;

    private HttpClient? httpClient;
    private Logger? logger;

    public Input MakeInput() => new TheInput();

    public Excavator MakeExcavator(string ticker, int timeframeInMinutes, PlaceToPut placeToPut) =>
        new TheExcavator(MakeHistoryOf(ticker, timeframeInMinutes), placeToPut, MakeLogger());

    public Logger MakeLogger() => logger ??= new ConsoleLogger();

    public Lara MakeLara() => new TheLara(this);

    private History MakeHistoryOf(string ticker, int timeframeInMinutes) => new MoexHistory(ticker, timeframeInMinutes,
        MakeHttpClient(), MakeCandlesParser(), MakeSplitsParser());

    private Parser<double[]> MakeSplitsParser() => new XmlSplitsParser();

    private HttpClient MakeHttpClient() => httpClient ??= new HttpClient();

    private Parser<Candle[]> MakeCandlesParser() => new XmlCandlesParser();

    public PlaceToPut MakeFilePlaceToPut(string ticker, int timeframeInMinutes) =>
        new TxtFile(ticker, timeframeInMinutes, MakeConfig());

    public SharesGetter MakeSharesGetter() => new TheSharesGetter(MakeHttpClient(), MakeSharesParser());

    private Parser<Share[]> MakeSharesParser() => new XmlSharesParser();

    public CandleBuffer MakeCandleBuffer() => new TheCandleBuffer();

    public ShareStatistics MakeShareStatistics() => new TheShareStatistics();

    public Output MakeOutput() => new ConsoleOutput();

    private Config MakeConfig() => new JsonConfig(MakeConstants());

    private Constants MakeConstants() => constants ??= new TheConstants();
}