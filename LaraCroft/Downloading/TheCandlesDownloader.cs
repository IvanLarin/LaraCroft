using LaraCroft.Entities;
using LaraCroft.Parsing;
using LaraCroft.ValueObjects;

namespace LaraCroft.Downloading;

internal class CandlesDownloaderProps
{
    public required int FromPosition { get; init; }

    public required Ticker Ticker { get; init; }
}

internal class TheCandlesDownloader(Interval interval, Parser<Candle[]> parser, BackDownloader downloader) : BaseDownloader<CandlesDownloaderProps, Candle[]>(parser, downloader), CandlesDownloader
{
    protected override Url GetUrl(CandlesDownloaderProps props) =>
        new($"https://iss.moex.com/iss/engines/stock/markets/shares/boards/TQBR/securities/{props.Ticker}/candles.json?interval={interval}&start={props.FromPosition}");
}