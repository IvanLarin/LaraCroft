using LaraCroft.Calculating;
using LaraCroft.Digging;
using LaraCroft.Downloading;
using LaraCroft.Entities;
using LaraCroft.Placing;
using LaraCroft.ProgressTracking;

namespace LaraCroft;

internal interface Factory :
    ShareProgressDisplayFactory,
    ExcavatorFactory,
    TrackerFactory<ShareProgress>,
    CandlesDownloaderFactory
{
    PlaceToPut<Candle> MakeFile(string ticker, int timeframeInMinutes);

    SharesDownloader MakeSharesDownloader(CancellationToken token = default);

    Place<Candle> MakeCandlePlace();

    VolumeCalculator MakeVolumeCalculator();

    Digger MakeDigger();
}