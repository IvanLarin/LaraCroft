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
    PlaceToPut<Candle[]> MakeCandlePlace(string ticker, int interval);

    SharesDownloader MakeSharesDownloader(CancellationToken token = default);

    Downloader MakeDownloader(CancellationToken token);

    Place<Candle[]> MakeInMemoryCandlePlace();

    VolumeCalculator MakeVolumeCalculator();

    public Digger<Candle[]> MakeCandleDigger(int interval);

    public PlaceToPut<(Spec, CandlesBorder)> MakeSpecPlace(string ticker);

    Digger<(Spec, CandlesBorder)> MakeSpecDigger(int interval);
}