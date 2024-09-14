using LaraCroft.Downloading;
using LaraCroft.Entities;
using LaraCroft.Parsing;

namespace LaraCroft.Chronology;

internal class MoexHistory(
    string ticker,
    Downloader downloader,
    Parser<Split[]> splitParser,
    CandlesDownloader candlesDownloader) : History
{
    private Split[]? allSplits;

    public async Task<Candle[]> GetCandles(int fromPosition)
    {
        await DownloadSplits();
        Candle[] candles = await candlesDownloader.Download(ticker, fromPosition);

        return AdjustVolumesForSplits(candles);
    }

    private async Task DownloadSplits() => allSplits ??= splitParser.Parse(await DownloadSplitText());

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

    private Task<string> DownloadSplitText() =>
        downloader.Download($"https://iss.moex.com/iss/statistics/engines/stock/splits/{ticker}.xml");
}