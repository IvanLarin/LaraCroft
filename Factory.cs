namespace LaraCroft;

public interface Factory
{
    Input MakeInput();

    History MakeHistoryOf(string ticker);

    Excavator MakeExcavator(string ticker);

    Logger MakeLogger();
}