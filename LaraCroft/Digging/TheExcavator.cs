using LaraCroft.Chronology;
using LaraCroft.Entities;
using LaraCroft.Placing;
using LaraCroft.ProgressTracking;

namespace LaraCroft.Digging;

internal class TheExcavator(
    PlaceToPut<Candle> placeToPut,
    string ticker,
    History history,
    ProgressTracker<ShareProgress> tracker) : Excavator
{
    public async Task Dig()
    {
        bool theEnd;
        var position = 0;

        do
        {
            Candle[] candles = await history.GetCandles(position);

            placeToPut.Put(candles);
            ReportProgress(candles);

            position += candles.Length;
            theEnd = !candles.Any();
        } while (!theEnd);
    }

    private void ReportProgress(Candle[] candles) => tracker.Report(new ShareProgress
    {
        Date = candles.Any() ? candles.Last().End : DateTime.Today,
        IsCompleted = !candles.Any(),
        Ticker = ticker
    });
}