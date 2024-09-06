using Common;

namespace LaraCroft;

internal class TheSharesGetter(Downloader downloader, Parser<Share[]> parser) : SharesGetter
{
    public Task<Share[]> GetShares() =>
        "https://iss.moex.com/iss/engines/stock/markets/shares/boards/TQBR/securities.xml".Pipe(downloader.Download)
            .Pipe(parser.Parse);
}