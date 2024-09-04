namespace LaraCroft;

internal class MoexHistory(string ticker, HttpClient httpClient, CandleParser candleParser, SplitParser splitParser) : History
{
    public async Task<Candle[]> GetCandles(int fromPosition)
    {
        string text = await DownloadCandleText(fromPosition);

        Candle[] candles = candleParser.Parse(text);

        Candle[] fixedCandles = await FixVolume(candles);

        return fixedCandles;
    }

    private async Task<string> DownloadTextFrom(string url)
    {
        HttpResponseMessage response = await httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
            throw new TerminateException($"Ошибка GET запроса на сервер по такому URL: \"{url}\"");

        string text = await response.Content.ReadAsStringAsync();

        return text;
    }

    private async Task<Candle[]> FixVolume(Candle[] candles)
    {

        double multiplier = await GetSplitMultiplier();

        bool needFixVolumeBecauseOfSplit = Math.Abs(1 - multiplier) > double.Epsilon;

        if (needFixVolumeBecauseOfSplit)
            return candles.Select(candle => candle with { Volume = candle.Volume / multiplier }).ToArray();

        return candles;
    }

    private double? splitMultiplier;

    private async Task<double> GetSplitMultiplier()
    {
        if (splitMultiplier.HasValue)
            return splitMultiplier.Value;

        double[] splits = await GetSplits();

        if (splits.Any())
        {
            double totalSplit = splits.Aggregate(1.0, (acc, x) => acc * x);
            splitMultiplier = totalSplit;
            return totalSplit;
        }

        splitMultiplier = 1;

        return 1;
    }

    private async Task<double[]> GetSplits()
    {
        string text = await DownloadSplitText();

        double[] splits = splitParser.Parse(text);

        return splits;
    }

    private Task<string> DownloadCandleText(int fromPosition) => DownloadTextFrom(
        $"https://iss.moex.com/iss/engines/stock/markets/shares/boards/TQBR/securities/{ticker}/candles.xml?interval=1&start={fromPosition}");

    private Task<string> DownloadSplitText() => DownloadTextFrom(
        $"https://iss.moex.com/iss/statistics/engines/stock/splits/{ticker}.xml");
}