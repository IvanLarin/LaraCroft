namespace LaraCroft;

internal interface CandleBuffer : PlaceToPut
{
    Candle[] Candles { get; }
}