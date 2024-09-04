namespace LaraCroft;

public class TheFactory : Factory
{
    private Logger? logger;

    private HttpClient? httpClient;

    public Input MakeInput() => new TheInput();

    public Excavator MakeExcavator(string ticker) => new TheExcavator(MakeHistoryOf(ticker), MakeStorage(ticker), MakeLogger());

    private History MakeHistoryOf(string ticker) => new MoexHistory(ticker, MakeHttpClient(), MakeCandleParser(), MakeSplitParser());

    private SplitParser MakeSplitParser() => new XmlSplitParser();

    private HttpClient MakeHttpClient() => httpClient ??= new HttpClient();

    private CandleParser MakeCandleParser() => new XmlCandleParser();

    private Storage MakeStorage(string ticker) => new TxtFile(ticker, MakeConfig());

    private Config MakeConfig() => new JsonConfig(MakeConstants());

    private Constants? constants;

    private Constants MakeConstants() => constants ??= new TheConstants();

    public Logger MakeLogger() => logger ??= new ConsoleLogger();
}