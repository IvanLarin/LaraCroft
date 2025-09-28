using LaraCroft.ValueObjects;

namespace LaraCroft.Inputting;

public interface Input
{
    Ticker[] GetTickers();
}