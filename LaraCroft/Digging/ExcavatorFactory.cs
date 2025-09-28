using LaraCroft.Entities;
using LaraCroft.Placing;
using LaraCroft.ProgressTracking;
using LaraCroft.ValueObjects;

namespace LaraCroft.Digging;

internal interface ExcavatorFactory
{
    Excavator MakeExcavator(PlaceToPut<Candle[]> placeToPut, Ticker ticker, Interval interval, ProgressTracker<ShareProgress> tracker, CancellationToken token);
}