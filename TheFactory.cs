namespace LaraCroft;

public class TheFactory : Factory
{
    private Logger? logger;

    private HttpClient? httpClient;

    public Input MakeInput() => new TheInput();

    public Excavator MakeExcavator(string ticker) => new TheExcavator(MakeHistoryOf(ticker), MakeStorage(ticker));

    private History MakeHistoryOf(string ticker) => new TheHistory(new TheHistory.TheHistoryParameters
    {
        Ticker = ticker,
        HttpClient = MakeHttpClient(),
        Logger = MakeLogger(),
        Parser = MakeParser(),
    });

    private HttpClient MakeHttpClient() => httpClient ??= new HttpClient();

    private Parser MakeParser() => new XmlParser();

    private Storage MakeStorage(string ticker) => new FileStorage(ticker, MakeConfig());

    private Config MakeConfig() => new JsonConfig(MakeConstants());

    private Constants? constants;

    private Constants MakeConstants() => constants ??= new TheConstants();

    public Logger MakeLogger() => logger ??= new TheLogger();
}