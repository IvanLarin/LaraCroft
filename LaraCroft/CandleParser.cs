namespace LaraCroft;

internal interface CandleParser
{
    Candle[] Parse(string text);
}