using LaraCroft.Entities;
using LaraCroft.Parsing;
using LaraCroft.ValueObjects;

namespace LaraCroft.Downloading;

internal class TheSpecDownloader(Parser<Spec> parser, BackDownloader downloader) : BaseDownloader<Ticker, Spec>(parser, downloader), SpecDownloader
{
    protected override Url GetUrl(Ticker ticker) =>
        new($"https://iss.moex.com/iss/engines/stock/markets/shares/boards/TQBR/securities/{ticker}.json");
}