namespace LaraCroft;

internal class MoexHistory(
    string ticker,
    int timeframeInMinutes,
    Downloader downloader,
    Parser<Candle[]> candleParser,
    Parser<Split[]> splitParser) : History
{
    private Split[]? allSplits;

    public async Task<Candle[]> GetCandles(int fromPosition)
    {
        await DownloadSplits();
        Candle[] candles = await DoGetCandles(fromPosition);

        return AdjustVolumesForSplits(candles);
    }

    private async Task DownloadSplits() => allSplits ??= splitParser.Parse(await DownloadSplitText());

    private Candle[] AdjustVolumesForSplits(Candle[] candles) => candles.Select(AdjustCandleVolume).ToArray();

    private Candle AdjustCandleVolume(Candle candle) =>
        candle with { Volume = CalculateNewVolumeOf(candle) };

    private long CalculateNewVolumeOf(Candle candle) =>
        Convert.ToInt64(Math.Round(candle.Volume / GetSplitFactorOnDate(candle.Begin)));

    private double GetSplitFactorOnDate(DateTime date) => CalculateSplitsFactor(Since(date));

    private double CalculateSplitsFactor(Split[] splits) => Multiply(splits.Select(GetSplitFactor));

    private double GetSplitFactor(Split split) => (double)split.Before / split.After;

    private double Multiply(IEnumerable<double> factors) => factors.Aggregate(1.0, func: (p, x) => p * x);

    private Split[] Since(DateTime date) => allSplits!.Where(split => date <= split.Date).ToArray();

    private async Task<Candle[]> DoGetCandles(int fromPosition) => candleParser.Parse(await DownloadCandleText(fromPosition));

    private Task<string> DownloadCandleText(int fromPosition) =>
        downloader.Download($"https://iss.moex.com/iss/engines/stock/markets/shares/boards/TQBR/securities/{ticker}/candles.xml?interval={timeframeInMinutes}&start={fromPosition}");

    private Task<string> DownloadSplitText() =>
        downloader.Download($"https://iss.moex.com/iss/statistics/engines/stock/splits/{ticker}.xml");
}