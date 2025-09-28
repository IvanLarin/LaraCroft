using LaraCroft.Entities;
using LaraCroft.Parsing;
using LaraCroft.ValueObjects;

namespace LaraCroft.Downloading;

internal class TheBorderDownloader(Parser<Border> parser, BackDownloader downloader) : BaseDownloader<Ticker, Border>(parser, downloader), BorderDownloader
{
    protected override Url GetUrl(Ticker ticker) =>
        new(
            $"https://iss.moex.com/iss/engines/stock/markets/shares/boards/TQBR/securities/{ticker}/candleborders.json");
}