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

    public Lara MakeLara() => new TheStyleOf(new TheLara(this), this);

    private History MakeHistoryOf(string ticker, int timeframeInMinutes) => new MoexHistory(ticker, timeframeInMinutes,
        MakeHttpClient(), MakeCandleParser(), MakeSplitParser());

    private SplitParser MakeSplitParser() => new XmlSplitParser();

    private HttpClient MakeHttpClient() => httpClient ??= new HttpClient();

    private CandleParser MakeCandleParser() => new XmlCandleParser();

    public PlaceToPut MakeFilePlaceToPut(string ticker, int timeframeInMinutes) =>
        new TxtFile(ticker, timeframeInMinutes, MakeConfig());

    private PlaceToPut MakeStatisticsPlaceToPut(string ticker) => throw new NotImplementedException();

    private Config MakeConfig() => new JsonConfig(MakeConstants());

    private Constants MakeConstants() => constants ??= new TheConstants();
}