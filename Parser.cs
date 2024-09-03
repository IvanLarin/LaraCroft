namespace LaraCroft;

public interface Parser
{
    IList<Candle> Parse(string text);
}