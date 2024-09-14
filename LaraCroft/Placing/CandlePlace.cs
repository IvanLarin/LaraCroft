using LaraCroft.Entities;

namespace LaraCroft.Placing;

internal class CandlePlace : Place<Candle>
{
    private Candle[] candles = [];

    public void Put(Candle[] theCandles) => candles = [.. candles, .. theCandles];

    public Candle[] Get() => [.. candles];
}