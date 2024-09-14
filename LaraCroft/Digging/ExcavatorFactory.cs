using LaraCroft.Entities;
using LaraCroft.Placing;
using LaraCroft.ProgressTracking;

namespace LaraCroft.Digging;

internal interface ExcavatorFactory
{
    Excavator MakeExcavator(PlaceToPut<Candle> placeToPut, string ticker, int timeframeInMinutes,
        ProgressTracker<ShareProgress> tracker,
        CancellationToken token = default);
}