namespace LaraCroft;

internal class TheCandleBuffer : CandleBuffer
{
    private Candle[] candles = [];

    public void Put(Candle[] theCandles) => candles = [.. candles, .. theCandles];

    public Candle[] Candles => [.. candles];
}