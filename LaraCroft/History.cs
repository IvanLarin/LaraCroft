namespace LaraCroft;

internal interface History
{
    Task<Candle[]> GetCandles(int fromPosition);
}