using LaraCroft.Entities;
using LaraCroft.Parsing;

namespace LaraCroft.Downloading;

internal class TheCandlesDownloader(
    int timeframeInMinutes,
    Downloader downloader,
    Parser<Candle[]> candleParser) : CandlesDownloader
{
    public async Task<Candle[]> Download(string ticker, int fromPosition) => candleParser.Parse(
        await downloader.Download(
            $"https://iss.moex.com/iss/engines/stock/markets/shares/boards/TQBR/securities/{ticker}/candles.xml?interval={timeframeInMinutes}&start={fromPosition}"));
}