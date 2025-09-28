using LaraCroft.Downloading;
using LaraCroft.Entities;
using LaraCroft.ValueObjects;

namespace LaraCroft.Chronology;

internal class MoexHistory(
    Ticker ticker,
    SplitsDownloader splitsDownloader,
    CandlesDownloader candlesDownloader,
    CancellationToken token) : History
{
    private Split[]? allSplits;

    public async Task<Candle[]> GetCandles(int fromPosition)
    {
        await DownloadSplits();
        Candle[] candles = await candlesDownloader.Download(new CandlesDownloaderProps { FromPosition = fromPosition, Ticker = ticker }, token);

        return AdjustVolumesForSplits(candles);
    }

    private async Task DownloadSplits() => allSplits ??= await splitsDownloader.Download(ticker, token);

    private Candle[] AdjustVolumesForSplits(Candle[] candles) =>
        candles.Select(AdjustCandleVolume).ToArray();

    private Candle AdjustCandleVolume(Candle candle) =>
        candle with { Volume = CalculateNewVolumeOf(candle) };

    private long CalculateNewVolumeOf(Candle candle) =>
        Convert.ToInt64(Math.Round(candle.Volume / GetSplitFactorOnDate(candle.Begin)));

    private double GetSplitFactorOnDate(DateTime date) => CalculateSplitsFactor(Since(date));

    private double CalculateSplitsFactor(Split[] splits) => Multiply(splits.Select(GetSplitFactor));

    private double GetSplitFactor(Split split) => (double)split.Before / split.After;

    private double Multiply(IEnumerable<double> factors) => factors.Aggregate(1.0, func: (p, x) => p * x);

    private Split[] Since(DateTime date) => allSplits!.Where(split => date <= split.Date).ToArray();
}