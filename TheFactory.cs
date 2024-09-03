namespace LaraCroft;

public class TheFactory : Factory
{
    private readonly Logger logger = new TheLogger();

    public Input MakeInput() => new JsonInput(logger);

    public History MakeHistoryOf(string ticker)
    {
        throw new NotImplementedException();
    }

    public Excavator MakeExcavator(string ticker)
    {
        throw new NotImplementedException();
    }
}