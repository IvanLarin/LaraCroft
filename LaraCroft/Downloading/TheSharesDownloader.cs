using LaraCroft.Entities;
using LaraCroft.Parsing;
using LaraCroft.ValueObjects;

namespace LaraCroft.Downloading;

internal class TheSharesDownloader(Parser<Share[]> parser, BackDownloader downloader) : BaseDownloader<Dummy, Share[]>(parser, downloader), SharesDownloader
{
    protected override Url GetUrl(Dummy props) =>
        new("https://iss.moex.com/iss/engines/stock/markets/shares/boards/TQBR/securities.xml");
}