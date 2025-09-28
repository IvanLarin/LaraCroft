using LaraCroft.Entities;
using LaraCroft.Parsing;
using LaraCroft.ValueObjects;

namespace LaraCroft.Downloading;

internal class TheSplitsDownloader(Parser<Split[]> splitsParser, BackDownloader downloader)
    : BaseDownloader<Ticker, Split[]>(splitsParser, downloader), SplitsDownloader
{
    protected override Url GetUrl(Ticker ticker) =>
        new($"https://iss.moex.com/iss/statistics/engines/stock/splits/{ticker}.json");
}