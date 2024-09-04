namespace LaraCroft;

public interface History
{
    Task<Candle[]> GetCandles(int fromPosition);
}