using Common;

namespace LaraCroft;

internal class MoexHistory(
    string ticker,
    int timeframeInMinutes,
    Downloader downloader,
    Parser<Candle[]> candleParser,
    Parser<Split[]> splitParser) : History
{
    private Split[]? allSplits;

    public Task<Candle[]> GetCandles(int fromPosition) => DoGetCandles(fromPosition).Pipe(AdjustVolumesForSplits);

    private Task<Candle[]> AdjustVolumesForSplits(Candle[] candles) =>
        candles.Select(AdjustCandleVolume).Pipe(Task.WhenAll);

    private async Task<Candle> AdjustCandleVolume(Candle candle) =>
        candle with { Volume = await candle.Pipe(CalculateNewVolume) };

    private async Task<long> CalculateNewVolume(Candle candle) =>
        (candle.Volume / await GetSplitFactorOnDate(candle.Begin))
        .Pipe(Math.Round)
        .Pipe(Convert.ToInt64);

    private Task<double> GetSplitFactorOnDate(DateTime date) => GetSplitsSince(date).Pipe(GetSplitsFactor);

    private double GetSplitsFactor(Split[] splits) => splits.Select(GetSplitFactor).Pipe(Multiply);

    private double GetSplitFactor(Split split) => (double)split.Before / split.After;

    private double Multiply(IEnumerable<double> factors) => factors.Aggregate(1.0, func: (p, x) => p * x);

    private async Task<Split[]> GetSplitsSince(DateTime date) =>
        (await GetAllSplits()).Where(split => date <= split.Date).ToArray();

    private async Task<Candle[]> DoGetCandles(int fromPosition) =>
        (await DownloadCandleText(fromPosition)).Pipe(candleParser.Parse);

    private async Task<Split[]> GetAllSplits() => allSplits ??= (await DownloadSplitText()).Pipe(splitParser.Parse);

    private Task<string> DownloadCandleText(int fromPosition) =>
        $"https://iss.moex.com/iss/engines/stock/markets/shares/boards/TQBR/securities/{ticker}/candles.xml?interval={timeframeInMinutes}&start={fromPosition}"
            .Pipe(downloader.Download);

    private Task<string> DownloadSplitText() =>
        $"https://iss.moex.com/iss/statistics/engines/stock/splits/{ticker}.xml"
            .Pipe(downloader.Download);
}