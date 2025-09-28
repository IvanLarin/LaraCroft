using LaraCroft.Calculating;
using LaraCroft.Digging;
using LaraCroft.Downloading;
using LaraCroft.Entities;
using LaraCroft.Placing;
using LaraCroft.ProgressTracking;
using LaraCroft.ValueObjects;

namespace LaraCroft;

internal interface Factory :
    ShareProgressDisplayFactory,
    ExcavatorFactory,
    TrackerFactory<ShareProgress>
{
    PlaceToPut<Candle[]> MakeCandlePlace(Ticker ticker, Interval interval);

    public Digger<Candle[]> MakeCandleDigger(Interval interval);

    public PlaceToPut<(Spec, Border)> MakeSpecPlace(Ticker ticker);

    Digger<(Spec, Border)> MakeSpecDigger(Interval interval);

    Place<Candle[]> MakeInMemoryCandlePlace();

    VolumeCalculator MakeVolumeCalculator();

    SharesDownloader MakeSharesDownloader();
}