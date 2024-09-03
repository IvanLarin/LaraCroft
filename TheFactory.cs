namespace LaraCroft;

public class TheFactory : Factory
{
    private Logger? logger;

    public Input MakeInput() => new TheInput();

    public History MakeHistoryOf(string ticker)
    {
        throw new NotImplementedException();
    }

    public Excavator MakeExcavator(string ticker)
    {
        throw new NotImplementedException();
    }

    public Logger MakeLogger() => logger ??= new TheLogger();
}