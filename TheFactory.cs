namespace LaraCroft;

public class TheFactory : Factory
{
    private Logger? logger;

    private HttpClient? httpClient;

    public Input MakeInput() => new TheInput();

    public Excavator MakeExcavator(string ticker) => new TheExcavator(MakeHistoryOf(ticker), MakeStorage(ticker), MakeLogger());

    private History MakeHistoryOf(string ticker) => new MoexHistory(ticker, MakeHttpClient(), MakeParser());

    private HttpClient MakeHttpClient() => httpClient ??= new HttpClient();

    private Parser MakeParser() => new XmlParser();

    private Storage MakeStorage(string ticker) => new TxtFile(ticker, MakeConfig());

    private Config MakeConfig() => new JsonConfig(MakeConstants());

    private Constants? constants;

    private Constants MakeConstants() => constants ??= new TheConstants();

    public Logger MakeLogger() => logger ??= new ConsoleLogger();
}