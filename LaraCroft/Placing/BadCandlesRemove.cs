using LaraCroft.Entities;

namespace LaraCroft.Placing;

internal class BadCandlesRemove(PlaceToPut<Candle[]> place) : PlaceToPut<Candle[]>
{
    private Candle[] buffer = [];

    public void Put(Candle[] candles)
    {
        buffer = [.. buffer, .. candles];

        while (BufferIsNotEmpty())
        {
            ShiftTradingBeginToFirstRoundCandleOfNewDay();

            if (ShiftFailed()) break;

            RemoveCandlesBeforeTradingBegin();
            FlushOneDay();
        }
    }

    private bool BufferIsNotEmpty() => buffer.Length > 0;

    private DateTime? tradingBegin;

    private bool ShiftFailed() => buffer.First().Begin.Date > tradingBegin?.Date;

    private void ShiftTradingBeginToFirstRoundCandleOfNewDay()
    {
        var first = buffer.FirstOrDefault(IsRoundCandle);

        if (first == null) return;

        if (first.Begin.Date == tradingBegin?.Date) return;

        tradingBegin = first.Begin;
    }

    private bool IsRoundCandle(Candle c) => c.Begin.Second == 0 && c.Begin.Minute % 10 == 0;

    private void RemoveCandlesBeforeTradingBegin() => buffer = buffer.Where(c => c.Begin >= tradingBegin).ToArray();

    private void FlushOneDay()
    {
        place.Put(buffer.Where(c => c.Begin.Date == buffer.First().Begin.Date).ToArray());

        buffer = buffer.Where(c => c.Begin.Date > buffer.First().Begin.Date).ToArray();
    }
}