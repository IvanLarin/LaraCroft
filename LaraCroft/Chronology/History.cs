using LaraCroft.Entities;

namespace LaraCroft.Chronology;

internal interface History
{
    Task<Candle[]> GetCandles(int fromPosition);
}