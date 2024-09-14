using LaraCroft.Entities;
using LaraCroft.Parsing;

namespace LaraCroft.Downloading;

internal class TheSharesDownloader(Downloader downloader, Parser<Share[]> parser) : SharesDownloader
{
    public async Task<Share[]> Download() => parser.Parse(await downloader.Download("https://iss.moex.com/iss/engines/stock/markets/shares/boards/TQBR/securities.xml"));
}