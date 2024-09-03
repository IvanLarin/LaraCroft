namespace LaraCroft;

public interface Factory
{
    Input MakeInput();

    Excavator MakeExcavator(string ticker);

    Logger MakeLogger();
}